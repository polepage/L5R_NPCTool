using NPC.Business.GameObjects;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IStorage
    {
        IManifest Database { get; }

        void Save(IGameObject gameObject);
        void Save(IEnumerable<IGameObject> gameObjects);

        IGameObject Open(IGameObjectMetadata metadata);
        IEnumerable<IGameObject> Open(IEnumerable<IGameObjectMetadata> metadata);

        void Delete(IGameObjectMetadata metadata);
        void Delete(IEnumerable<IGameObjectMetadata> metadata);
    }
}
