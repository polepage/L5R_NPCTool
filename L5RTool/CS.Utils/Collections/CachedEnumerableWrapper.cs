using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CS.Utils.Collections
{
    public class CachedEnumerableWrapper<T, TSource>: EnumerableWrapper<T, TSource>
    {
        private readonly int _cacheSize = 128;
        private List<T> _cache;

        private Func<TSource, T, bool> _lookup;

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup)
            : base(collection, converter)
        {
            InitCache();
            _lookup = lookup;
        }
        
        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Func<TSource, bool> sourceFilter)
            : base(collection, converter, sourceFilter)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Comparison<T> comparer)
            : base(collection, converter, comparer)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Func<T, bool> filter)
            : base(collection, converter, filter)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection, converter, comparer, filter)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Func<TSource, bool> sourceFilter, Comparison<T> comparer)
            : base(collection, converter, sourceFilter, comparer)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Func<TSource, bool> sourceFilter, Func<T, bool> filter)
            : base(collection, converter, sourceFilter, filter)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, Func<TSource, bool> sourceFilter, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection, converter, sourceFilter, comparer, filter)
        {
            InitCache();
            _lookup = lookup;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize)
            : base(collection, converter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Func<TSource, bool> sourceFilter)
            : base(collection, converter, sourceFilter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Comparison<T> comparer)
            : base(collection, converter, comparer)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Func<T, bool> filter)
            : base(collection, converter, filter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection, converter, comparer, filter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Func<TSource, bool> sourceFilter, Comparison<T> comparer)
            : base(collection, converter, sourceFilter, comparer)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Func<TSource, bool> sourceFilter, Func<T, bool> filter)
            : base(collection, converter, sourceFilter, filter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        public CachedEnumerableWrapper(IEnumerable<TSource> collection, Func<TSource, T> converter, Func<TSource, T, bool> lookup, int cacheSize, Func<TSource, bool> sourceFilter, Comparison<T> comparer, Func<T, bool> filter)
            : base(collection, converter, sourceFilter, comparer, filter)
        {
            InitCache();
            _lookup = lookup;
            _cacheSize = cacheSize;
        }

        protected override void OnAdd(IList addedItems, int index)
        {
            base.OnAdd(addedItems, index);
            CheckCache();
        }

        protected override void OnRemove(IList removedItems, int index)
        {
            base.OnRemove(removedItems, index);
            CheckCache();
        }

        protected override void OnReset()
        {
            base.OnReset();
            CheckCache();
        }

        protected override T Convert(TSource source)
        {
            var converted = _cache.FirstOrDefault(t => _lookup(source, t));
            if (converted == null)
            {
                converted = base.Convert(source);
                _cache.Add(converted);
            }

            return converted;
        }

        private void InitCache()
        {
            _cache = new List<T>(this);
        }

        private void CheckCache()
        {
            if (_cache.Count <= _cacheSize || _cache.Count == Count)
            {
                return;
            }

            int excess = Math.Min(_cache.Count - _cacheSize,
                                  _cache.Count - Count);

            var toRemove = _cache.Where(c => !this.Contains(c)).Take(excess).ToList();
            foreach (T item in toRemove)
            {
                _cache.Remove(item);
            }
        }
    }
}
