using NPC.Model;

namespace NPC.Business
{
    static class ElementFactory
    {
        public static IElement Create(ElementType type)
        {
            switch (type)
            {
                case ElementType.Demeanor:
                    return new Demeanor();
                default:
                    return null;
            }
        }
    }
}
