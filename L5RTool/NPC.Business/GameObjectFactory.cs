using System.Collections.Generic;
using System.Linq;
using NPC.Business.GameObjects;
using NPC.Common;

namespace NPC.Business
{
    class GameObjectFactory : IGameObjectFactory
    {
        private IStorage _storage;
        private Data.IGameObjectFactory _dataObjectFactory;

        public GameObjectFactory(IStorage storage, Data.IGameObjectFactory dataObjectFactory)
        {
            _storage = storage;
            _dataObjectFactory = dataObjectFactory;
        }

        public IGameObject CreateNewObject(ObjectType type)
        {
            return new GameObject(_dataObjectFactory.Create(type));
        }

        public IGameObject DuplicateObject(IGameObject targetObject)
        {
            return new GameObject(_dataObjectFactory.Create(targetObject.Type), targetObject);
        }

        public IGameObject DuplicateReference(IObjectReference objectReference)
        {
            return DuplicateObject(_storage.Open(objectReference));
        }

        public IEnumerable<IGameObject> DuplicateReference(IEnumerable<IObjectReference> objectReferences)
        {
            return objectReferences.Select(or => DuplicateReference(or));
        }
    }
}
