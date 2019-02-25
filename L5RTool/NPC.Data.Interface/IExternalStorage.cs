using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IExternalStorage
    {
        void Export(IEnumerable<IGameObjectReference> references, string target);
    }
}
