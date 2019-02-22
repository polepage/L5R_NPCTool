using NPC.Common;

namespace NPC.Business.GameObjects
{
    public interface IGameObject: IGameObjectReference
    {
        IGameObjectData Data { get; }
        ObjectType Type { get; }
        string Name { get; set; }
        bool IsDirty { get; }
    }
}
