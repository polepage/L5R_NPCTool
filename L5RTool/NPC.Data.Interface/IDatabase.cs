using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IDatabase
    {
        IEnumerable<IGameObjectMetadata> GameObjects { get; }
    }
}
