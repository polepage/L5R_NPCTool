using NPC.Common;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    public interface IGameObjectFactory
    {
        IGameObject Create(ObjectType type);
    }
}
