using NPC.Model;

namespace NPC.Business
{
    class ElementFactory: IElementFactory
    {
        public IElement CreateElement(ElementType type)
        {
            switch (type)
            {
                case ElementType.Demeanor:
                    return new Demeanor();
                case ElementType.Trait:
                    return new Trait();
                default:
                    return null;
            }
        }
    }
}
