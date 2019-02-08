using Prism.Commands;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace L5RUI.Controls
{
    [TemplatePart(Name="PART_Selector", Type=typeof(Selector))]
    class TagSelector: MultiSelector
    {
        #region Templates
        public static readonly DependencyProperty PromptTemplateProperty =
            DependencyProperty.Register("PromptTemplate",
                                        typeof(DataTemplate),
                                        typeof(TagSelector));

        public DataTemplate PromptTemplate
        {
            get => (DataTemplate)GetValue(PromptTemplateProperty);
            set => SetValue(PromptTemplateProperty, value);
        }

        public static readonly DependencyProperty PromptTemplateSelectorProperty =
            DependencyProperty.Register("PromptTemplateSelector",
                                        typeof(DataTemplateSelector),
                                        typeof(TagSelector));

        public DataTemplateSelector PromptTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(PromptTemplateSelectorProperty);
            set => SetValue(PromptTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty PromptStringFormatProperty =
            DependencyProperty.Register("PromptStringFormat",
                                        typeof(string),
                                        typeof(TagSelector));

        public string PromptStringFormat
        {
            get => (string)GetValue(PromptStringFormatProperty);
            set => SetValue(PromptStringFormatProperty, value);
        }

        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register("SelectedItemTemplate",
                                        typeof(DataTemplate),
                                        typeof(TagSelector));

        public DataTemplate SelectedItemTemplate
        {
            get => (DataTemplate)GetValue(SelectedItemTemplateProperty);
            set => SetValue(SelectedItemTemplateProperty, value);
        }

        public static readonly DependencyProperty SelectedItemTemplateSelectorProperty =
            DependencyProperty.Register("SelectedItemTemplateSelector",
                                        typeof(DataTemplateSelector),
                                        typeof(TagSelector));

        public DataTemplateSelector SelectedItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(SelectedItemTemplateSelectorProperty);
            set => SetValue(SelectedItemTemplateSelectorProperty, value);
        }

        public static readonly DependencyProperty SelectedItemStringFormatProperty =
            DependencyProperty.Register("SelectedItemStringFormat",
                                        typeof(string),
                                        typeof(TagSelector));

        public string SelectedItemStringFormat
        {
            get => (string)GetValue(SelectedItemStringFormatProperty);
            set => SetValue(SelectedItemStringFormatProperty, value);
        }
        #endregion

        #region Prompt
        public static readonly DependencyProperty PromptProperty =
            DependencyProperty.Register("Prompt",
                                        typeof(object),
                                        typeof(TagSelector),
                                        new PropertyMetadata("Add"));

        public object Prompt
        {
            get => GetValue(PromptProperty);
            set => SetValue(PromptProperty, value);
        }
        #endregion

        #region Items
        private static readonly DependencyPropertyKey AvailableItemsPropertyKey =
            DependencyProperty.RegisterReadOnly("AvailableItems",
                                                typeof(IEnumerable),
                                                typeof(TagSelector),
                                                new PropertyMetadata());

        public static readonly DependencyProperty AvailableItemsProperty = AvailableItemsPropertyKey.DependencyProperty;

        public IEnumerable AvailableItems
        {
            get => (IEnumerable)GetValue(AvailableItemsProperty);
            private set => SetValue(AvailableItemsPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CurrentSelectionPropertyKey =
            DependencyProperty.RegisterReadOnly("CurrentSelection",
                                                typeof(IEnumerable),
                                                typeof(TagSelector),
                                                new PropertyMetadata());

        public static readonly DependencyProperty CurrentSelectionProperty = CurrentSelectionPropertyKey.DependencyProperty;

        public IEnumerable CurrentSelection
        {
            get => (IEnumerable)GetValue(CurrentSelectionProperty);
            private set => SetValue(CurrentSelectionPropertyKey, value);
        }
        #endregion

        #region Selection
        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.Register("BindableSelectedItems",
                                        typeof(IList),
                                        typeof(TagSelector),
                                        new PropertyMetadata(OnBindableSelectedItemsChanged));

        public IList BindableSelectedItems
        {
            get => (IList)GetValue(BindableSelectedItemsProperty);
            set => SetValue(BindableSelectedItemsProperty, value);
        }
        #endregion

        #region Commands
        private static readonly DependencyPropertyKey UnselectCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("UnselectCommand",
                                                typeof(ICommand),
                                                typeof(TagSelector),
                                                new PropertyMetadata());

        public static readonly DependencyProperty UnselectCommandProperty = UnselectCommandPropertyKey.DependencyProperty;

        public ICommand UnselectCommand
        {
            get => (ICommand)GetValue(UnselectCommandProperty);
            private set => SetValue(UnselectCommandPropertyKey, value);
        }
        #endregion

        private Selector _selectorPart;

        private bool _externalSelectionUpdate;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UnInitParts();
            _selectorPart = Template.FindName("PART_Selector", this) as Selector;
            InitParts();

            UnselectCommand = new DelegateCommand<object>(o => SelectedItems.Remove(o));
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            UpdateItemsAvailability();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            UpdateItemsAvailability();
            PushListUpdate(BindableSelectedItems, e.AddedItems, e.RemovedItems);
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tagSelector = d as TagSelector;
            if (tagSelector == null)
            {
                return;
            }

            tagSelector.UnsubscribeCollectionObserver(e.OldValue as INotifyCollectionChanged);
            tagSelector.SubscribeCollectionObserver(e.NewValue as INotifyCollectionChanged);

            tagSelector.BeginUpdateSelectedItems();
            tagSelector.SelectedItems.Clear();
            tagSelector.PushListUpdate(tagSelector.SelectedItems, e.NewValue as IList, null);
            tagSelector.EndUpdateSelectedItems();
        }

        private void UpdateItemsAvailability()
        {
            AvailableItems = ItemsSource.Cast<object>().Where(o => !SelectedItems.Contains(o));
            CurrentSelection = ItemsSource.Cast<object>().Where(o => SelectedItems.Contains(o));

            if (_selectorPart != null)
            {
                _selectorPart.IsEnabled = AvailableItems.Cast<object>().Count() > 0;
            }
        }

        private void PushListUpdate(IList target, IList added, IList removed)
        {
            if (target == null)
            {
                return;
            }

            if (removed != null)
            {
                foreach (object item in removed)
                {
                    target.Remove(item);
                }
            }

            if (added != null)
            {
                foreach (object item in added)
                {
                    target.Add(item);
                }
            }
        }

        private void SelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BeginUpdateSelectedItems();
            PushListUpdate(SelectedItems, e.AddedItems, null);
            EndUpdateSelectedItems();
        }

        private void BindableSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BeginUpdateSelectedItems();
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                SelectedItems.Clear();
            }
            else
            {
                PushListUpdate(SelectedItems, e.NewItems, e.OldItems);
            }
            EndUpdateSelectedItems();
        }

        private void InitParts()
        {
            _selectorPart.SelectionChanged += SelectorSelectionChanged;
        }
        
        private void UnInitParts()
        {
            if (_selectorPart != null)
            {
                _selectorPart.SelectionChanged -= SelectorSelectionChanged;
            }
        }

        private void SubscribeCollectionObserver(INotifyCollectionChanged collection)
        {
            if (collection != null)
            {
                collection.CollectionChanged += BindableSelectionChanged;
            }
        }

        private void UnsubscribeCollectionObserver(INotifyCollectionChanged collection)
        {
            if (collection != null)
            {
                collection.CollectionChanged -= BindableSelectionChanged;
            }
        }
    }
}
