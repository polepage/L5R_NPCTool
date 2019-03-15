using NPC.Common;

namespace NPC.Data.GameObjects
{
    public interface IGameObject : IGameObjectReference
    {
        ObjectType Type { get; }
        string Name { get; set; }
        bool IsDirty { get; }
    }
}
