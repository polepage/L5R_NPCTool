using L5RUI.ViewModels.Elements;
using NPC.Model;
using System.Windows;
using System.Windows.Controls;

namespace L5RUI.Templates
{
    class ElementTemplateSelector: DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate DemeanorTemplate { get; set; }
        public DataTemplate TraitTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = item as IElementViewModel;
            if (element != null)
            {
                switch (element.Type)
                {
                    case ElementType.Demeanor:
                        return DemeanorTemplate;
                    case ElementType.Trait:
                        return TraitTemplate;
                    default:
                        break;
                }
            }

            return DefaultTemplate;
        }
    }
}
