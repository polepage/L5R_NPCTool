using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace CS.Utils.Collections
{
    public class RelayObservableHashSet<T, TSource>: ObservableHashSet<T>
    {
        private Func<TSource, T> _converter;

        public RelayObservableHashSet(IEnumerable<TSource> source, Func<TSource, T> converter)
            : base(source.Select(s => converter(s)))
        {
            _converter = converter;
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
                    UnionWith(e.NewItems.Cast<TSource>().Select(s => _converter(s)));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    ExceptWith(e.OldItems.Cast<TSource>().Select(s => _converter(s)));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    SymmetricExceptWith(e.NewItems.Cast<TSource>()
                                            .Concat(e.NewItems.Cast<TSource>())
                                            .Select(s => _converter(s)));
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
