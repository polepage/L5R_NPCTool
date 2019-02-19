using System;
using System.Collections.Generic;
using System.Linq;
using NPC.Business.GameObjects;

namespace NPC.Business
{
    class Storage : IStorage
    {
        private Data.IStorage _storage;

        public Storage(Data.IStorage storage)
        {
            _storage = storage;
            Database = new ReferenceDatabase(storage.Database);
        }

        public IReferenceDatabase Database { get; }

        public void Save(IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                _storage.Save(go.Source);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            _storage.Save(gameObjects.OfType<GameObject>().Select(s => s.Source));
        }

        public IGameObject Open(IObjectReference objectReference)
        {
            if (objectReference is ObjectReference reference)
            {
                return new GameObject(_storage.Open(reference.Source));
            }

            throw new ArgumentException("Business.Storage: Can't open reference. Reference don't exist.");
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IObjectReference> objectReferences)
        {
            return objectReferences.Select(or => Open(or));
        }
    }
}
