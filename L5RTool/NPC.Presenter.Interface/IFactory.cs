using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;

namespace NPC.Presenter
{
    public interface IFactory
    {
        IGameObject CreateNew(ObjectType type);

        IGameObject Duplicate(IGameObjectReference reference);
        IEnumerable<IGameObject> Duplicate(IEnumerable<IGameObjectReference> references);

        void CopyTo(IGameObject target, IGameObjectReference source);
    }
}
