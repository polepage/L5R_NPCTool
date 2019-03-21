using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.GameObjects;
using System.Windows;

namespace NPC.Presenter.Windows.Controls
{
    class TemplateElementPresenter : CompositeObjectPresenter
    {
        #region Character Template
        public static readonly DependencyProperty CharacterTemplateProperty =
            DependencyProperty.Register("CharacterTemplate",
                                        typeof(ITemplate),
                                        typeof(TemplateElementPresenter));

        public ITemplate CharacterTemplate
        {
            get => (ITemplate)GetValue(CharacterTemplateProperty);
            set => SetValue(CharacterTemplateProperty, value);
        }
        #endregion

        protected override void AddElement()
        {
            switch (ElementType)
            {
                case CharacterElement.Advantage:
                    CharacterTemplate.AddAdvantage();
                    break;
                case CharacterElement.Disadvantage:
                    CharacterTemplate.AddDisadvantage();
                    break;
                case CharacterElement.Demeanor:
                    CharacterTemplate.AddDemeanor();
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
                    CharacterTemplate.RemoveAdvantage(element as IAdvantage);
                    break;
                case CharacterElement.Disadvantage:
                    CharacterTemplate.RemoveDisadvantage(element as IDisadvantage);
                    break;
                case CharacterElement.Demeanor:
                    CharacterTemplate.RemoveDemeanor(element as IDemeanor);
                    break;
                default:
                    break;
            }
        }
    }
}
