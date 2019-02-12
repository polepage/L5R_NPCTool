using NPC.Common;
using System.ComponentModel;

namespace NPC.Presenter.GameObjects
{
    public interface IGameObject : INotifyPropertyChanged
    {
        ObjectType Type { get; }
        bool IsDirty { get; }
    }
}
