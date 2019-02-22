using NPC.Common;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    public interface IFactory
    {
        IGameObject Create(ObjectType type);
    }
}
