using NPC.Common;

namespace NPC.Business.GameObjects
{
    public interface IGameObjectMetadata: IGameObjectReference
    {
        ObjectType Type { get; }
        string Name { get; }
    }
}
