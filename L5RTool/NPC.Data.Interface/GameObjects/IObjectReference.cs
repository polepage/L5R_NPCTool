using NPC.Common;
using System.ComponentModel;

namespace NPC.Data.GameObjects
{
    public interface IObjectReference: INotifyPropertyChanged
    {
        ObjectType Type { get; }
        string Name { get; }
    }
}
