using NPC.Business.GameObjects;
using NPC.Common;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IFactory
    {
        IGameObject CreateNew(ObjectType type);

        IGameObject Duplicate(IGameObjectReference reference);
        IEnumerable<IGameObject> Duplicate(IEnumerable<IGameObjectReference> references);
    }
}
