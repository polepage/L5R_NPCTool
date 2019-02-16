using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CS.Utils.Collections
{
    public class RelayObservableHashSet<T, TSource>: ObservableHashSet<T>
    {
        private Func<TSource, T> _converter;
        private Func<TSource, bool> _filter;

        public RelayObservableHashSet(IEnumerable<TSource> source, Func<TSource, T> converter)
            : this (source, converter, s => true)
        {
        }

        public RelayObservableHashSet(IEnumerable<TSource> source, Func<TSource, T> converter, Func<TSource, bool> filter)
            : base(source.Where(filter).Select(converter))
        {
            _converter = converter;
            _filter = filter;

            if (source is INotifyCollectionChanged ncc)
            {
                ncc.CollectionChanged += SourceChanged;
            }
        }

        private void SourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    UnionWith(e.NewItems
                        .Cast<TSource>()
                        .Where(_filter)
                        .Select(_converter));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ExceptWith(e.OldItems
                        .Cast<TSource>()
                        .Where(_filter)
                        .Select(_converter));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    SymmetricExceptWith(e.NewItems
                        .Cast<TSource>()
                        .Concat(e.NewItems
                            .Cast<TSource>())
                        .Where(_filter)
                        .Select(_converter));
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
