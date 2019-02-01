using NPC.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace L5RUI.Model.Database
{
    class Group: BindableBase
    {
        public Group(ElementType type)
        {
            Type = type;
            Entries = new ObservableCollection<Entry>();
        }

        public ElementType Type { get; }
        public IList<Entry> Entries { get; }
    }
}
