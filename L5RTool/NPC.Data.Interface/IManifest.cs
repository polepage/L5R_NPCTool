using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IManifest
    {
        IEnumerable<IGameObjectMetadata> GameObjects { get; }
    }
}
