using NPC.Model;

namespace NPC
{
    public interface INPC
    {
        IElement CreateElement(ElementType type);
    }
}
