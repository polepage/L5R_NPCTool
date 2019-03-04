using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace CS.Utils.Collections
{
    public class EnumerableWrapper<T> : BindableBase, IReadOnlyList<T>, INotifyCollectionChanged
    {
        private ObservableCollection<T> _collection;
        private Comparison<T> _comparer;
        private Func<T, bool> _filter;

        public EnumerableWrapper(IEnumerable<T> collection)
            : this(collection, null, null, collection as INotifyCollectionChanged)
        {
        }

        public EnumerableWrapper(IEnumerable<T> collection, Comparison<T> comparer)
            : this(collection, comparer, null, collection as INotifyCollectionChanged)
        {
        }

        public EnumerableWrapper(IEnumerable<T> collection, Func<T, bool> filter)
            : this(collection, null, filter, collection as INotifyCollectionChanged)
        {
        }

        public EnumerableWrapper(IEnumerable<T> collection, Comparison<T> comparer, Func<T, bool> filter)
            : this(collection,  comparer, filter, collection as INotifyCollectionChanged)
        {
        }

        protected EnumerableWrapper(IEnumerable<T> collection, Comparison<T> comparer, Func<T, bool> filter, INotifyCollectionChanged source)
        {
            _comparer = comparer;
            _filter = filter;

            IEnumerable<T> collectionSource = collection;
            if (_filter != null)
            {
                collectionSource = collectionSource.Where(_filter);
            }
            
            if (_comparer != null)
            {
                foreach (INotifyPropertyChanged item in collectionSource.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += ItemPropertyChanged;
                }

                var orderedSource = new List<T>(collectionSource);
                orderedSource.Sort(_comparer);
                collectionSource = orderedSource;
            }

            _collection = new ObservableCollection<T>(collectionSource);

            source.CollectionChanged += SourceChanged;
        }

        public T this[int index] => _collection[index];
        public int Count => _collection.Count;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _collection.CollectionChanged += value; }
            remove { _collection.CollectionChanged -= value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual void OnAdd(IList addedItems, int index)
        {
            IEnumerable<T> newItems = addedItems.Cast<T>();
            if (_filter != null)
            {
                newItems = newItems.Where(_filter);
            }

            if (_comparer != null)
            {
                foreach (T item in newItems)
                {
                    if (item is INotifyPropertyChanged propertyChanger)
                    {
                        propertyChanger.PropertyChanged += ItemPropertyChanged;
                    }

                    for (int i = 0; i < _collection.Count; i++)
                    {
                        if (_comparer(item, _collection[i]) < 0)
                        {
                            _collection.Insert(i, item);
                            goto ItemAdded;
                        }
                    }

                    _collection.Add(item);

                ItemAdded:;
                }
            }
            else
            {
                int i = index < 0 ? _collection.Count : index;
                foreach (T item in newItems)
                {
                    _collection.Insert(i, item);
                    i++;
                }
            }
        }

        protected virtual void OnRemove(IList removedItems, int index)
        {
            IEnumerable<T> oldItems = removedItems.Cast<T>();
            if (_filter != null)
            {
                oldItems = oldItems.Where(_filter);
            }

            if (_comparer != null)
            {
                foreach (INotifyPropertyChanged item in oldItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }

            if (index < 0)
            {
                foreach (T item in oldItems)
                {
                    _collection.Remove(item);
                }
            }
            else
            {
                for (int i = 0; i < oldItems.Count(); i++)
                {
                    _collection.RemoveAt(index);
                }
            }
        }

        protected virtual void OnReset()
        {
            if (_comparer != null)
            {
                foreach (INotifyPropertyChanged item in _collection.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }

            _collection.Clear();
        }

        protected int FindIndex(T item)
        {
            return _collection.IndexOf(item);
        }

        private void ReorderCollection()
        {
            if (_comparer == null)
            {
                return;
            }

            QuickSort(0, _collection.Count - 1);
        }

        private void QuickSort(int startIndex, int endIndex)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            int pivot = startIndex;
            int currentOffset = 1;

            while (pivot + currentOffset <= endIndex)
            {
                if (_comparer(_collection[pivot], _collection[pivot + currentOffset]) > 0)
                {
                    _collection.Move(pivot + currentOffset, pivot);
                    pivot++;
                }
                else
                {
                    currentOffset++;
                }
            }

            QuickSort(startIndex, pivot - 1);
            QuickSort(pivot + 1, endIndex);
        }

        private void SourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAdd(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnRemove(e.OldItems, e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    OnRemove(e.OldItems, e.OldStartingIndex);
                    OnAdd(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    OnRemove(e.NewItems, e.OldStartingIndex);
                    OnAdd(e.NewItems, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    OnReset();
                    break;
                default:
                    break;
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ReorderCollection();
        }
    }

    public class EnumerableWrapper<T, TSource> : EnumerableWrapper<T>
    {
        private Func<TSource, T> _converter;
        private Func<TSource, bool> _filter;

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter)
            : base(collection.Select(converter), null, null, collection as INotifyCollectionChanged)
        {
            _converter = converter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, bool> sourceFilter)
            : base(collection.Where(sourceFilter).Select(converter), null, null, collection as INotifyCollectionChanged)
        {
            _converter = converter;
            _filter = sourceFilter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Comparison<T> comparer)
            : base(collection.Select(converter), comparer, null, collection as INotifyCollectionChanged)
        {
            _converter = converter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<T, bool> filter)
            : base(collection.Select(converter), null, filter, collection as INotifyCollectionChanged)
        {
            _converter = converter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection.Select(converter), comparer, filter, collection as INotifyCollectionChanged)
        {
            _converter = converter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, bool> sourceFilter, Comparison<T> comparer)
            : base(collection.Where(sourceFilter).Select(converter), comparer, null, collection as INotifyCollectionChanged)
        {
            _converter = converter;
            _filter = sourceFilter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, bool> sourceFilter, Func<T, bool> filter)
            : base(collection.Where(sourceFilter).Select(converter), null, filter, collection as INotifyCollectionChanged)
        {
            _converter = converter;
            _filter = sourceFilter;
        }

        public EnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, bool> sourceFilter, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection.Where(sourceFilter).Select(converter), comparer, filter, collection as INotifyCollectionChanged)
        {
            _converter = converter;
            _filter = sourceFilter;
        }

        protected override void OnAdd(IList addedItems, int index)
        {
            IEnumerable<TSource> newItems = addedItems.Cast<TSource>();
            if (_filter != null)
            {
                newItems = newItems.Where(_filter);
            }

            base.OnAdd(newItems.Select(Convert).ToList(), index);
        }

        protected override void OnRemove(IList removedItems, int index)
        {
            IEnumerable<TSource> oldItems = removedItems.Cast<TSource>();
            if (_filter != null)
            {
                oldItems = oldItems.Where(_filter);
            }

            base.OnRemove(oldItems.Select(Convert).ToList(), index);
        }

        protected virtual T Convert(TSource source)
        {
            return _converter(source);
        }
    }
}
