using NPC.Model;

namespace NPC
{
    public interface IElementFactory
    {
        IElement CreateElement(ElementType type);
    }
}
