using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Templates
{
    class ElementTemplateSelector: DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate DemeanorTemplate { get; set; }
        public DataTemplate TraitTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var gameObject = item as IGameObject;
            if (gameObject != null)
            {
                switch (gameObject.Type)
                {
                    case ObjectType.Advantage:
                    case ObjectType.Disadvantage:
                        return TraitTemplate;
                    default:
                        break;
                }
            }

            return DefaultTemplate;
        }
    }
}
