using System.Collections.Generic;
using CS.Utils.Collections;
using NPC.Business.GameObjects;

namespace NPC.Business
{
    class Database : IDatabase
    {
        private RelayObservableHashSet<IGameObjectMetadata, Data.GameObjects.IGameObjectMetadata> _collection;

        public Database(Data.IDatabase database)
        {
            _collection = new RelayObservableHashSet<IGameObjectMetadata, Data.GameObjects.IGameObjectMetadata>(
                database.GameObjects,
                r => new GameObjectMetadata(r));
        }

        public IEnumerable<IGameObjectMetadata> GameObjects => _collection;
    }
}
