using CS.Utils.Collections;
using NPC.Common;
using Prism.Mvvm;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    class ObjectMetadataGroup: BindableBase
    {
        private EnumerableWrapper<IGameObjectMetadata> _gameObjects;

        public ObjectMetadataGroup(ObjectType type, IEnumerable<IGameObjectMetadata> gameObjects)
        {
            Type = type;

            _gameObjects = new EnumerableWrapper<IGameObjectMetadata>(
                gameObjects,
                (o1, o2) => { return o1.Name.CompareTo(o2.Name); },
                o => o.Type == Type);
        }

        public ObjectType Type { get; }
        public IEnumerable<IGameObjectMetadata> GameObjects => _gameObjects;
    }
}
