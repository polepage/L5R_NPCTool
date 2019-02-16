using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NPC.Presenter.Windows.Collections
{
    class SetWrapper<T> : ISet<T>, IList, INotifyCollectionChanged
    {
        private ISet<T> _source;

        private int _syncCount;

        public SetWrapper(ISet<T> source)
        {
            _source = source;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                if (_source is INotifyCollectionChanged ncc)
                {
                    ncc.CollectionChanged += value;
                    _syncCount++;
                }
            }
            remove
            {
                if (_source is INotifyCollectionChanged ncc)
                {
                    ncc.CollectionChanged -= value;
                    _syncCount--;
                }
            }
        }

        public object this[int index]
        {
            get => _source.ElementAt(index);
            set
            {
                T toRemove = _source.ElementAt(index);
                _source.SymmetricExceptWith(new List<T> { toRemove, (T)value });
            }
        }

        public int Count => _source.Count;
        public bool IsReadOnly => (_source as ICollection<T>)?.IsReadOnly ?? false;
        public bool IsFixedSize => false;
        public object SyncRoot => this;
        public bool IsSynchronized => _syncCount > 0;

        public bool Add(T item)
        {
            return _source.Add(item);
        }

        public int Add(object value)
        {
            if (Add((T)value))
            {
                return IndexOf((T)value);
            }

            return -1;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Insert(int index, object value)
        {
            Add(value);
        }

        public bool Remove(T item)
        {
            return _source.Remove(item);
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public void RemoveAt(int index)
        {
            Remove(this[index]);
        }

        public void Clear()
        {
            _source.Clear();
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _source.ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _source.SymmetricExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            _source.IntersectWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _source.UnionWith(other);
        }

        public bool Contains(T item)
        {
            return _source.Contains(item);
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public int IndexOf(object value)
        {
            int i = 0;
            foreach(T item in _source)
            {
                if (item.Equals(value))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _source.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _source.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _source.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _source.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _source.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _source.SetEquals(other);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _source.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
