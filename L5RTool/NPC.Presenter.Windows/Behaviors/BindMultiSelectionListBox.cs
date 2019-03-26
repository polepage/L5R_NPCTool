using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Behaviors
{
    class BindMultiSelectionListBox: Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems",
                                        typeof(IList),
                                        typeof(BindMultiSelectionListBox),
                                        new PropertyMetadata(OnSelectedItemsChanged));

        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        private static readonly DependencyPropertyKey IsSelectorObserverSubscribedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsSelectorObserverSubscribed",
                                                typeof(bool),
                                                typeof(BindMultiSelectionListBox),
                                                new PropertyMetadata());

        private static readonly DependencyProperty IsSelectorObserverSubscribedProperty = IsSelectorObserverSubscribedPropertyKey.DependencyProperty;

        private bool IsSelectorObserverSubscribed
        {
            get => (bool)GetValue(IsSelectorObserverSubscribedProperty);
            set => SetValue(IsSelectorObserverSubscribedPropertyKey, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (SelectedItems != null)
            {
                SubscribeCollectionObserver();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            UnsubscribeSelectorObserver();
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is BindMultiSelectionListBox behavior))
            {
                return;
            }

            behavior.UnsubscribeCollectionObserver(e.OldValue as INotifyCollectionChanged);
            behavior.SubscribeCollectionObserver(e.NewValue as INotifyCollectionChanged);

            behavior.UnsubscribeSelectorObserver();
            behavior.Clear(behavior.AssociatedObject?.SelectedItems);
            behavior.PushUpdate(behavior.AssociatedObject?.SelectedItems, e.NewValue as IList, null);

            if (e.NewValue != null)
            {
                behavior.SubscribeSelectorObserver();
            }
        }

        private void SubscribeSelectorObserver()
        {
            if (AssociatedObject != null && !IsSelectorObserverSubscribed)
            {
                AssociatedObject.SelectionChanged += SelectorSelectionChanged;
                IsSelectorObserverSubscribed = true;
            }
        }

        private void UnsubscribeSelectorObserver()
        {
            if (AssociatedObject != null && IsSelectorObserverSubscribed)
            {
                AssociatedObject.SelectionChanged -= SelectorSelectionChanged;
                IsSelectorObserverSubscribed = false;
            }
        }

        private void SubscribeCollectionObserver()
        {
            SubscribeCollectionObserver(SelectedItems as INotifyCollectionChanged);
        }

        private void SubscribeCollectionObserver(INotifyCollectionChanged collection)
        {
            if (collection != null)
            {
                collection.CollectionChanged += CollectionSelectionChanged;
            }
        }

        private void UnsubscribeCollectionObserver()
        {
            UnsubscribeCollectionObserver(SelectedItems as INotifyCollectionChanged);
        }

        private void UnsubscribeCollectionObserver(INotifyCollectionChanged collection)
        {
            if (collection != null)
            {
                collection.CollectionChanged -= CollectionSelectionChanged;
            }
        }

        private void SelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnsubscribeCollectionObserver();
            PushUpdate(SelectedItems, e.AddedItems, e.RemovedItems);
            SubscribeCollectionObserver();
        }

        private void CollectionSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeSelectorObserver();

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Clear(AssociatedObject?.SelectedItems);
            }
            else
            {
                PushUpdate(AssociatedObject?.SelectedItems, e.NewItems, e.OldItems);
            }

            SubscribeSelectorObserver();
        }

        private void PushUpdate(IList target, IList addedItems, IList removedItems)
        {
            if (removedItems != null)
            {
                foreach (object item in removedItems)
                {
                    target?.Remove(item);
                }
            }
            
            if (addedItems != null)
            {
                foreach (object item in addedItems)
                {
                    target?.Add(item);
                }
            }
        }

        private void Clear(IList target)
        {
            target?.Clear();
        }
    }
}
