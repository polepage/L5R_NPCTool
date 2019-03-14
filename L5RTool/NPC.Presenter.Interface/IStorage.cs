using NPC.Presenter.GameObjects;
using System.Collections.Generic;

namespace NPC.Presenter
{
    public interface IStorage
    {
        IManifest Database { get; }

        void Save(IGameObject gameObject);
        void Save(IEnumerable<IGameObject> gameObjects);

        IGameObject Open(IGameObjectReference metadata);
        IEnumerable<IGameObject> Open(IEnumerable<IGameObjectReference> metadata);

        void Delete(IGameObjectReference metadata);
        void Delete(IEnumerable<IGameObjectReference> metadata);
    }
}
