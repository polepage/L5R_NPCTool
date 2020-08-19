using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Behaviors
{
    class PersistentTabControl: Behavior<TabControl>
    {
        #region Templates
        public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(
            "ContentTemplateSelector",
            typeof(DataTemplateSelector),
            typeof(PersistentTabControl));

        public DataTemplateSelector ContentTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty);
            set => SetValue(ContentTemplateSelectorProperty, value);
        }
        #endregion

        #region ItemSource
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register(
            "ItemSource",
            typeof(IEnumerable),
            typeof(PersistentTabControl),
            new PropertyMetadata(OnItemSourceChanged));

        public IEnumerable ItemSource
        {
            get => (IEnumerable)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PersistentTabControl behavior)
            {
                behavior.UnregisterItemSourceEvents(e.OldValue as INotifyCollectionChanged);
                behavior.RegisterItemSourceEvents(e.NewValue as INotifyCollectionChanged);
                behavior.LoadItemSourceContent(e.NewValue as IEnumerable);
            }
        }

        private void RegisterItemSourceEvents(INotifyCollectionChanged newCollection)
        {
            if (newCollection != null)
            {
                newCollection.CollectionChanged += ItemSourceContentChanged;
            }
        }

        private void UnregisterItemSourceEvents(INotifyCollectionChanged oldCollection)
        {
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= ItemSourceContentChanged;
            }
        }

        private void ItemSourceContentChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object data in e.NewItems)
                    {
                        AddTab(data);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object data in e.OldItems)
                    {
                        RemoveTab(data);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ClearTabs();
                    break;
                default:
                    // Move and Replace not supported
                    break;
            }
        }

        private void LoadItemSourceContent(IEnumerable source)
        {
            if (AssociatedObject == null)
            {
                return;
            }

            if (source == null)
            {
                return;
            }

            ClearTabs();
            foreach (object data in source)
            {
                AddTab(data);
            }

            if (SelectedItem != null)
            {
                SetSelectedTab(SelectedItem);
            }
        }

        private void AddTab(object data)
        {
            var content = new ContentControl();
            content.SetBinding(ContentControl.ContentProperty, new System.Windows.Data.Binding());
            content.ContentTemplateSelector = ContentTemplateSelector;

            var item = new TabItem
            {
                DataContext = data,
                Content = content
            };

            AssociatedObject.Items.Add(item);

            if (AssociatedObject.SelectedItem == null)
            {
                item.IsSelected = true;
            }
        }

        private void RemoveTab(object data)
        {
            var item = AssociatedObject.Items.Cast<TabItem>().FirstOrDefault(ti => ti.DataContext == data);
            if (item != null)
            {
                AssociatedObject.Items.Remove(item);
            }
        }

        private void ClearTabs()
        {
            AssociatedObject.Items.Clear();
        }
        #endregion

        #region SelectedItem
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(object),
            typeof(PersistentTabControl),
            new PropertyMetadata(OnSelectedItemChanged));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PersistentTabControl behavior)
            {
                behavior.SetSelectedTab(e.NewValue);
            }
        }

        private void SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is TabItem selectedTab)
            {
                foreach (object data in ItemSource)
                {
                    if (selectedTab.DataContext == data)
                    {
                        SelectedItem = data;
                        return;
                    }
                }
            }
        }

        private void SetSelectedTab(object data)
        {
            if (data == null)
            {
                AssociatedObject.SelectedItem = null;
                return;
            }

            if (AssociatedObject.Items.Cast<TabItem>().FirstOrDefault(ti => ti.DataContext == data) is TabItem toSelect &&
                !toSelect.IsSelected)
            {
                toSelect.IsSelected = true;
            }
        }
        #endregion

        protected override void OnAttached()
        {
            LoadItemSourceContent(ItemSource);
            AssociatedObject.SelectionChanged += SelectedTabChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= SelectedTabChanged;
        }
    }
}
