using NPC.Common;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class GameObjectFactory : IGameObjectFactory
    {
        public IGameObject Create(ObjectType type)
        {
            return new GameObject(type);
        }
    }
}
