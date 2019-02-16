using NPC.Business.GameObjects;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IStorage
    {
        IReferenceDatabase Database { get; }

        void Save(IGameObject gameObject);
        void Save(IEnumerable<IGameObject> gameObjects);
    }
}
