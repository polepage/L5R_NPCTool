using NPC.Business.GameObjects;
using NPC.Common;

namespace NPC.Business
{
    public interface IGameObjectFactory
    {
        IGameObject CreateNewObject(ObjectType type);
    }
}
