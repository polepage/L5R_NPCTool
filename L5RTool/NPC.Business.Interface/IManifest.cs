using NPC.Business.GameObjects;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IManifest
    {
        IEnumerable<IGameObjectMetadata> GameObjects { get; }
    }
}
