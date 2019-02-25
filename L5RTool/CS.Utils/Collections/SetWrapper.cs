using System;
using System.Collections.Generic;

namespace CS.Utils.Collections
{
    public class SetWrapper<T>: CollectionWrapper<T>, ISet<T>
    {
        private ISet<T> _set;

        public SetWrapper(ISet<T> set)
            : base(set)
        {
            _set = set;
        }

        public SetWrapper(ISet<T> set, Comparison<T> comparer)
            : base(set, comparer)
        {
            _set = set;
        }

        public SetWrapper(ISet<T> set, Func<T, bool> filter)
            : base(set, filter)
        {
            _set = set;
        }

        public SetWrapper(ISet<T> set, Comparison<T> comparer, Func<T, bool> filter)
            : base(set, comparer, filter)
        {
            _set = set;
        }

        bool ISet<T>.Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }

            AddItem(item);
            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            _set.ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            _set.SymmetricExceptWith(other);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            _set.IntersectWith(other);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            _set.UnionWith(other);
        }

        protected override void AddItem(T item)
        {
            if (!_set.Contains(item))
            {
                base.AddItem(item);
            }
        }
    }
}
