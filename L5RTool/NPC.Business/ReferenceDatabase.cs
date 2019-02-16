using System.Collections.Generic;
using CS.Utils.Collections;
using NPC.Business.GameObjects;

namespace NPC.Business
{
    class ReferenceDatabase : IReferenceDatabase
    {
        private RelayObservableHashSet<IObjectReference, Data.GameObjects.IObjectReference> _collection;

        public ReferenceDatabase(Data.IReferenceDatabase database)
        {
            _collection = new RelayObservableHashSet<IObjectReference, Data.GameObjects.IObjectReference>(
                database.References,
                r => new ObjectReference(r));
        }

        public IEnumerable<IObjectReference> References => _collection;
    }
}
