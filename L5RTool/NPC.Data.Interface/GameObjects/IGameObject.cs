using NPC.Common;
using System.ComponentModel;

namespace NPC.Data.GameObjects
{
    public interface IGameObject : INotifyPropertyChanged
    {
        ObjectType Type { get; }
        bool IsDirty { get; }
    }
}
