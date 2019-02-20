using NPC.Business.GameObjects;
using NPC.Common;
using System.Collections.Generic;

namespace NPC.Business
{
    public interface IGameObjectFactory
    {
        IGameObject CreateNewObject(ObjectType type);

        IGameObject DuplicateObject(IGameObject targetObject);
        IGameObject DuplicateReference(IObjectReference objectReference);
        IEnumerable<IGameObject> DuplicateReference(IEnumerable<IObjectReference> objectReferences);
    }
}
