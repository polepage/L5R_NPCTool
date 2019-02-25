using System;
using System.Collections;
using System.Collections.Generic;

namespace CS.Utils.Collections
{
    public class CollectionWrapper<T>: EnumerableWrapper<T>, IList<T>, IList
    {
        ICollection<T> _collection;

        public CollectionWrapper(ICollection<T> collection)
            : base(collection)
        {
            _collection = collection;
        }

        public CollectionWrapper(ICollection<T> collection, Comparison<T> comparer)
            : base(collection, comparer)
        {
            _collection = collection;
        }

        public CollectionWrapper(ICollection<T> collection, Func<T, bool> filter)
            : base(collection, filter)
        {
            _collection = collection;
        }

        public CollectionWrapper(ICollection<T> collection, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection, comparer, filter)
        {
            _collection = collection;
        }

        T IList<T>.this[int index]
        {
            get => base[index];
            set => AddItem(value);
        }

        object IList.this[int index]
        {
            get => base[index];
            set => AddItem((T)value);
        }

        public bool IsReadOnly => _collection.IsReadOnly;
        public bool IsFixedSize => (_collection is IList list) ? list.IsFixedSize : false;
        public bool IsSynchronized => (_collection is ICollection col) ? col.IsSynchronized : true;
        public object SyncRoot => (_collection is ICollection col) ? col.SyncRoot : this;

        public void Add(T item)
        {
            AddItem(item);
        }

        public int Add(object value)
        {
            Add((T)value);
            return IndexOf((T)value);
        }

        public void Insert(int index, T item)
        {
            Add(item);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public bool Remove(T item)
        {
            return _collection.Remove(item);
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
            _collection.Clear();
        }

        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public int IndexOf(T item)
        {
            return FindIndex(item);
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        protected virtual void AddItem(T item)
        {
            _collection.Add(item);
        }
    }
}
