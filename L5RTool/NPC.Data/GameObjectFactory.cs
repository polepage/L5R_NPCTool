using NPC.Common;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class GameObjectFactory : IGameObjectFactory
    {
        private InternalFactory _factory;

        public GameObjectFactory(InternalFactory internalFactory)
        {
            _factory = internalFactory;
        }

        public IGameObject Create(ObjectType type)
        {
            return _factory.CreateEmpty(type);
        }
    }
}
