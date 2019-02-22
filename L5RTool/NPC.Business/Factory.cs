using System.Collections.Generic;
using System.Linq;
using NPC.Business.GameObjects;
using NPC.Common;

namespace NPC.Business
{
    class Factory : IFactory
    {
        private IStorage _storage;
        private Data.IFactory _factory;

        public Factory(IStorage storage, Data.IFactory factory)
        {
            _storage = storage;
            _factory = factory;
        }

        public IGameObject CreateNew(ObjectType type)
        {
            return new GameObject(_factory.Create(type));
        }

        public IGameObject Duplicate(IGameObjectReference reference)
        {
            var go = reference as IGameObject;
            if (go == null)
            {
                go = _storage.Open(reference as IGameObjectMetadata);
            }

            return DuplicateObject(go);
        }

        public IEnumerable<IGameObject> Duplicate(IEnumerable<IGameObjectReference> references)
        {
            return references.Select(r => Duplicate(r));
        }

        private IGameObject DuplicateObject(IGameObject targetObject)
        {
            return new GameObject(_factory.Create(targetObject.Type), targetObject);
        }
    }
}
