using System.Collections.Generic;

namespace NPC
{
    public interface IElementStorage
    {
        void Save(IElement element);
        void Save(IEnumerable<IElement> elements);
    }
}
