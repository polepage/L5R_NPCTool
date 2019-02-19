using NPC.Common;
using System.ComponentModel;

namespace NPC.Business.GameObjects
{
    public interface IGameObject: INotifyPropertyChanged
    {
        IGameObjectData Data { get; }
        ObjectType Type { get; }
        string Name { get; set; }
        bool IsDirty { get; }
    }
}
