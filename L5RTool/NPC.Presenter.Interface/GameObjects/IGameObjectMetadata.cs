using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    public interface IGameObjectMetadata: IGameObjectReference
    {
        ObjectType Type { get; }
        string Name { get; }
    }
}
