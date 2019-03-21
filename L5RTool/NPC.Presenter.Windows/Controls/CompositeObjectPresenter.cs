using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.GameObjects;
using Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Controls
{
    class CompositeObjectPresenter: ItemsControl
    {
        #region ElementTemplate
        public static readonly DependencyProperty ElementTemplateProperty =
            DependencyProperty.Register("ElementTemplate",
                                        typeof(DataTemplate),
                                        typeof(CompositeObjectPresenter));

        public DataTemplate ElementTemplate
        {
            get => (DataTemplate)GetValue(ElementTemplateProperty);
            set => SetValue(ElementTemplateProperty, value);
        }
        #endregion

        #region Type
        public static readonly DependencyProperty ElementTypeProperty =
            DependencyProperty.Register("ElementType",
                                        typeof(CharacterElement),
                                        typeof(CompositeObjectPresenter));

        public CharacterElement ElementType
        {
            get => (CharacterElement)GetValue(ElementTypeProperty);
            set => SetValue(ElementTypeProperty, value);
        }
        #endregion

        #region Add Command
        private static readonly DependencyPropertyKey AddCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("AddCommand",
                                                typeof(ICommand),
                                                typeof(CompositeObjectPresenter),
                                                new PropertyMetadata());

        public static readonly DependencyProperty AddCommandProperty = AddCommandPropertyKey.DependencyProperty;

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            private set => SetValue(AddCommandPropertyKey, value);
        }
        #endregion

        #region Remove Command
        private static readonly DependencyPropertyKey RemoveCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("RemoveCommand",
                                                typeof(ICommand),
                                                typeof(CompositeObjectPresenter),
                                                new PropertyMetadata());

        public static readonly DependencyProperty RemoveCommandProperty = RemoveCommandPropertyKey.DependencyProperty;

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            private set => SetValue(RemoveCommandPropertyKey, value);
        }
        #endregion

        public CompositeObjectPresenter()
        {
            AddCommand = new DelegateCommand(AddElement);
            RemoveCommand = new DelegateCommand<IGameObject>(RemoveElement);
        }

        protected virtual void AddElement() { }
        protected virtual void RemoveElement(IGameObject element) { }
    }
}
