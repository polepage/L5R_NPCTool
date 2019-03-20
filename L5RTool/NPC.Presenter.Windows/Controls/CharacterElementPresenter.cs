using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.GameObjects;
using Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Controls
{
    class CharacterElementPresenter: ItemsControl
    {
        #region Character
        public static readonly DependencyProperty CharacterProperty =
            DependencyProperty.Register("Character",
                                        typeof(ICharacter),
                                        typeof(CharacterElementPresenter));

        public ICharacter Character
        {
            get => (ICharacter)GetValue(CharacterProperty);
            set => SetValue(CharacterProperty, value);
        }
        #endregion

        #region ElementTemplate
        public static readonly DependencyProperty ElementTemplateProperty =
            DependencyProperty.Register("ElementTemplate",
                                        typeof(DataTemplate),
                                        typeof(CharacterElementPresenter));

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
                                        typeof(CharacterElementPresenter));

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
                                                typeof(CharacterElementPresenter),
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
                                                typeof(CharacterElementPresenter),
                                                new PropertyMetadata());

        public static readonly DependencyProperty RemoveCommandProperty = RemoveCommandPropertyKey.DependencyProperty;

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            private set => SetValue(RemoveCommandPropertyKey, value);
        }
        #endregion

        public CharacterElementPresenter()
        {
            AddCommand = new DelegateCommand(AddElement);
            RemoveCommand = new DelegateCommand<IGameObject>(RemoveElement);
        }

        private void AddElement()
        {
            switch (ElementType)
            {
                case CharacterElement.Advantage:
                    Character.AddAdvantage();
                    break;
                case CharacterElement.Disadvantage:
                    Character.AddDisadvantage();
                    break;
                case CharacterElement.FavoredWeapon:
                    Character.AddFavoredWeapon();
                    break;
                case CharacterElement.EquippedGear:
                    Character.AddEquipedGear();
                    break;
                case CharacterElement.OtherGear:
                    Character.AddOtherGear();
                    break;
                case CharacterElement.Ability:
                    Character.AddAbility();
                    break;
                default:
                    break;
            }
        }

        private void RemoveElement(IGameObject element)
        {
            switch (ElementType)
            {
                case CharacterElement.Advantage:
                    Character.RemoveAdvantage(element as IAdvantage);
                    break;
                case CharacterElement.Disadvantage:
                    Character.RemoveDisadvantage(element as IDisadvantage);
                    break;
                case CharacterElement.FavoredWeapon:
                    Character.RemoveFavoredWeapon(element as IGear);
                    break;
                case CharacterElement.EquippedGear:
                    Character.RemoveEquipedGear(element as IGear);
                    break;
                case CharacterElement.OtherGear:
                    Character.RemoveOtherGear(element as IGear);
                    break;
                case CharacterElement.Ability:
                    Character.RemoveAbility(element as IAbility);
                    break;
                default:
                    break;
            }
        }
    }
}
