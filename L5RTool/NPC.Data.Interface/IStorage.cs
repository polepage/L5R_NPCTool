using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IStorage
    {
        IManifest Manifest { get; }

        void Save(IGameObject gameObject);
        void Save(IEnumerable<IGameObject> gameObjects);

        IGameObject Open(IGameObjectMetadata metadata);
        IEnumerable<IGameObject> Open(IEnumerable<IGameObjectMetadata> metadata);

        void Delete(IGameObjectMetadata metadata);
        void Delete(IEnumerable<IGameObjectMetadata> metadata);
    }
}
