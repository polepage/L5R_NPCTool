using NPC.Business.GameObjects;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IExternalStorage
    {
        void Import(string target);
        void Export(IEnumerable<IGameObjectReference> references, string target);
    }
}
