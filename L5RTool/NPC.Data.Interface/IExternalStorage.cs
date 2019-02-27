using NPC.Data.GameObjects;
using System;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IExternalStorage
    {
        void Import(string target);
        void Export(IEnumerable<IGameObjectReference> references, string target);
    }
}
