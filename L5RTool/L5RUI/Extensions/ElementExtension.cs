using L5RUI.ViewModels.Elements;
using NPC;
using NPC.Model;

namespace L5RUI.Extensions
{
    static class ElementExtension
    {
        public static IElementViewModel CreateViewModel(this IElement element)
        {
            switch (element.Type)
            {
                case ElementType.Demeanor:
                    return new DemeanorElement(element as Demeanor);
                case ElementType.Trait:
                    return new TraitElement(element as Trait);
                default:
                    return null;
            }
        }
    }
}
