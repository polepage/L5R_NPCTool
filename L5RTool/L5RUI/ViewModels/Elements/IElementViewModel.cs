using NPC;
using NPC.Model;

namespace L5RUI.ViewModels.Elements
{
    public interface IElementViewModel
    {
        ElementType Type { get; }
        IElement Element { get; }
    }
}
