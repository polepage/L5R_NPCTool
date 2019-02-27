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
            Database = new Manifest(storage.Manifest);
        }

        public IManifest Database { get; }

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

        public IGameObject Open(IGameObjectMetadata metadata)
        {
            if (metadata is GameObjectMetadata go)
            {
                return new GameObject(_storage.Open(go.Source));
            }

            throw new ArgumentException("Business.Storage: Can't open metadata. Reference don't exist.");
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IGameObjectMetadata> metadata)
        {
            return metadata.Select(go => Open(go));
        }

        public void Delete(IGameObjectMetadata metadata)
        {
            if (metadata is GameObjectMetadata go)
            {
                _storage.Delete(go.Source);
            }

            throw new ArgumentException("Business.Storage: Can't delete metadata. Reference don't exist.");
        }

        public void Delete(IEnumerable<IGameObjectMetadata> metadata)
        {
            _storage.Delete(metadata
                .OfType<GameObjectMetadata>()
                .Select(go => go.Source));
        }
    }
}
