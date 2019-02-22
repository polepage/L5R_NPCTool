using NPC.Business.GameObjects;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IDatabase
    {
        IEnumerable<IGameObjectMetadata> GameObjects { get; }
    }
}
