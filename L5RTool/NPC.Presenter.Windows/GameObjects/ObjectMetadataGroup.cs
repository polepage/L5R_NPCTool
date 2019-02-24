using CS.Utils.Collections;
using NPC.Common;
using Prism.Mvvm;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    class ObjectMetadataGroup: BindableBase
    {
        private EnumerableWrapper<IGameObjectMetadata, Business.GameObjects.IGameObjectMetadata> _gameObjects;

        public ObjectMetadataGroup(ObjectType type, IEnumerable<Business.GameObjects.IGameObjectMetadata> gameObjects)
        {
            Type = type;

            _gameObjects = new EnumerableWrapper<IGameObjectMetadata, Business.GameObjects.IGameObjectMetadata>(
                gameObjects, go => new GameObjectMetadata(go), go => go.Type == Type, (o1, o2) =>
                {
                    return o1.Name.CompareTo(o2.Name);
                });
        }

        public ObjectType Type { get; }
        public IEnumerable<IGameObjectMetadata> GameObjects => _gameObjects;
    }
}
