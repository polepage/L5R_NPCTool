using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;

namespace CS.Utils.Collections
{
    public class ObservableHashSet<T>: ISet<T>, ICollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IDeserializationCallback, ISerializable, INotifyCollectionChanged
    {
        private readonly HashSet<T> _set;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableHashSet()
        {
            _set = new HashSet<T>();
        }

        public ObservableHashSet(IEnumerable<T> collection)
        {
            _set = new HashSet<T>(collection);
        }

        public ObservableHashSet(IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(comparer);
        }

        public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(collection, comparer);
        }

        public int Count => _set.Count;
        public bool IsReadOnly => (_set as ICollection<T>)?.IsReadOnly ?? false;

        public bool Add(T item)
        {
            bool hasChanged = _set.Add(item);
            if (hasChanged)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                                                                         new List<T> { item }));
            }

            return hasChanged;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public bool Remove(T item)
        {
            bool hasChanged = _set.Remove(item);
            if (hasChanged)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                                                                         new List<T> { item }));
            }

            return hasChanged;
        }

        public void Clear()
        {
            bool isChanging = Count > 0;
            _set.Clear();
            if (isChanging)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("ObservableHashSet.ExceptWith: collection is null.");
            }

            IList<T> itemsToRemove = other.Where(t => _set.Contains(t)).ToList();
            _set.ExceptWith(other);
            if (itemsToRemove.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                                                                         new List<T>(itemsToRemove)));
            }
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("ObservableHashSet.SymmetricExceptWith: collection is null.");
            }

            IList<T> itemsToRemove = other.Where(t => _set.Contains(t)).ToList();
            IList<T> itemsToAdd = other.Where(t => !_set.Contains(t)).ToList();
            _set.SymmetricExceptWith(other);
            if (itemsToAdd.Count > 0 && itemsToRemove.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,
                                                                         new List<T>(itemsToAdd), new List<T>(itemsToRemove)));
            }
            else if (itemsToAdd.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                                                                         new List<T>(itemsToAdd)));
            }
            else if (itemsToRemove.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                                                                         new List<T>(itemsToRemove)));
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("ObservableHashSet.UnionWith: collection is null.");
            }

            var itemsToAdd = other.Where(t => !_set.Contains(t)).ToList();
            _set.UnionWith(other);
            if (itemsToAdd.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                                                                         new List<T>(itemsToAdd)));
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("ObservableHashSet.IntersectionWith: collection is null.");
            }

            IList<T> itemsToRemove = _set.Where(t => !other.Contains(t)).ToList();
            _set.IntersectWith(other);
            if (itemsToRemove.Count > 0)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                                                                         new List<T>(itemsToRemove)));
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _set.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            _set.OnDeserialization(sender);
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }
    }
}
