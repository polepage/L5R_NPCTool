using NPC.Common;
using System.ComponentModel;

namespace NPC.Presenter.GameObjects
{
    public interface IObjectReference: INotifyPropertyChanged
    {
        ObjectType Type { get; }
        string Name { get; }
    }
}
