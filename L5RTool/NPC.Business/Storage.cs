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
            var goSource = gameObject as IGameObjectSource;
            if (goSource != null)
            {
                _storage.Save(goSource.SourceObject);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            _storage.Save(gameObjects.OfType<IGameObjectSource>().Select(s => s.SourceObject));
        }
    }
}
