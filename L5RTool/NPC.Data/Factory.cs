using NPC.Common;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class Factory : IFactory
    {
        public IGameObject Create(ObjectType type)
        {
            return new GameObject(type);
        }
    }
}
