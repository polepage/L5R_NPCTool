using NPC.Business.GameObjects;
using NPC.Common;

namespace NPC.Business
{
    class GameObjectFactory : IGameObjectFactory
    {
        private InternalFactory _factory;
        private Data.IGameObjectFactory _dataObjectFactory;

        public GameObjectFactory(InternalFactory internalFactory, Data.IGameObjectFactory dataObjectFactory)
        {
            _factory = internalFactory;
            _dataObjectFactory = dataObjectFactory;
        }

        public IGameObject CreateNewObject(ObjectType type)
        {
            return _factory.Create(_dataObjectFactory.Create(type));
        }
    }
}
