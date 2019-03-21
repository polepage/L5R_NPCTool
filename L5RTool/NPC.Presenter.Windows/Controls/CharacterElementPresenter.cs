using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.GameObjects;
using System.Windows;

namespace NPC.Presenter.Windows.Controls
{
    class CharacterElementPresenter: CompositeObjectPresenter
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

        protected override void AddElement()
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

        protected override void RemoveElement(IGameObject element)
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
