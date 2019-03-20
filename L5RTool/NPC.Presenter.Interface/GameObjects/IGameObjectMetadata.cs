using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface IGameObjectMetadata: IGameObjectReference
    {
        ObjectType Type { get; }
        string Name { get; }
        IEnumerable<string> Keywords { get; }
    }
}
