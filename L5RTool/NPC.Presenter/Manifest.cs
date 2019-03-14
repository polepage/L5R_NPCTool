using CS.Utils.Collections;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;

namespace NPC.Presenter
{
    class Manifest: IManifest
    {
        private RelayObservableHashSet<IGameObjectMetadata, Data.GameObjects.IGameObjectMetadata> _collection;

        public Manifest(Data.IManifest database)
        {
            _collection = new RelayObservableHashSet<IGameObjectMetadata, Data.GameObjects.IGameObjectMetadata>(
                database.GameObjects,
                r => new GameObjectMetadata(r));
        }

        public IEnumerable<IGameObjectMetadata> GameObjects => _collection;
    }
}
