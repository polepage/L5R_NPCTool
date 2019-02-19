using NPC.Data.GameObjects;
using System.Collections.Generic;

namespace NPC.Data
{
    public interface IStorage
    {
        IReferenceDatabase Database { get; }

        void Save(IGameObject gameObject);
        void Save(IEnumerable<IGameObject> gameObjects);

        IGameObject Open(IObjectReference objectReference);
        IEnumerable<IGameObject> Open(IEnumerable<IObjectReference> objectReferences);
    }
}
