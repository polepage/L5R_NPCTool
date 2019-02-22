using CS.Utils.Collections;
using NPC.Common;
using Prism.Mvvm;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    class ObjectMetadataGroup: BindableBase
    {
        private RelayObservableHashSet<IGameObjectMetadata, Business.GameObjects.IGameObjectMetadata> _gameObjects;

        public ObjectMetadataGroup(ObjectType type, IEnumerable<Business.GameObjects.IGameObjectMetadata> gameObjects)
        {
            Type = type;

            _gameObjects = new RelayObservableHashSet<IGameObjectMetadata, Business.GameObjects.IGameObjectMetadata>(
                gameObjects, or => new GameObjectMetadata(or), or => or.Type == Type);
        }

        public ObjectType Type { get; }
        public IEnumerable<IGameObjectMetadata> GameObjects => _gameObjects;
    }
}
