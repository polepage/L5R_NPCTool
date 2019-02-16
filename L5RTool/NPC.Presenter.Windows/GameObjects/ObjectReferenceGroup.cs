using CS.Utils.Collections;
using NPC.Common;
using Prism.Mvvm;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    class ObjectReferenceGroup: BindableBase
    {
        private RelayObservableHashSet<IObjectReference, Business.GameObjects.IObjectReference> _references;

        public ObjectReferenceGroup(ObjectType type, IEnumerable<Business.GameObjects.IObjectReference> references)
        {
            Type = type;

            _references = new RelayObservableHashSet<IObjectReference, Business.GameObjects.IObjectReference>(
                references, or => new ObjectReference(or), or => or.Type == Type);
        }

        public ObjectType Type { get; }
        public IEnumerable<IObjectReference> References => _references;
    }
}
