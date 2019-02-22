using NPC.Common;
using System.ComponentModel;

namespace NPC.Data.GameObjects
{
    public interface IGameObjectMetadata: IGameObjectReference
    {
        ObjectType Type { get; }
        string Name { get; }
    }
}
