using NPC.Business.GameObjects;
using NPC.Common;

namespace NPC.Business
{
    class GameObjectFactory : IGameObjectFactory
    {
        private Data.IGameObjectFactory _dataObjectFactory;

        public GameObjectFactory(Data.IGameObjectFactory dataObjectFactory)
        {
            _dataObjectFactory = dataObjectFactory;
        }

        public IGameObject CreateNewObject(ObjectType type)
        {
            return new GameObject(_dataObjectFactory.Create(type));
        }
    }
}
