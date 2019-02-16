using NPC.Common;
using Prism.Mvvm;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    class ObjectReferenceGroup: BindableBase
    {
        public ObjectReferenceGroup(ObjectType type, IEnumerable<IObjectReference> references)
        {
            Type = type;
            References = references;
        }

        public ObjectType Type { get; }
        public IEnumerable<IObjectReference> References { get; }

        public void UpdateReferences()
        {
            RaisePropertyChanged(nameof(References));
        }
    }
}
