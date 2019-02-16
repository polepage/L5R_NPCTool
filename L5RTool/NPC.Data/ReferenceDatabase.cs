using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CS.Utils.Collections;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class ReferenceDatabase : IReferenceDatabase
    {
        private ObservableHashSet<ObjectReference> _collection;

        public ReferenceDatabase()
        {
            _collection = new ObservableHashSet<ObjectReference>();
        }

        public IEnumerable<IObjectReference> References => _collection;

        public bool AddOrModify(ObjectReference reference)
        {
            if (_collection.FirstOrDefault(or => or.Equals(reference)) is ObjectReference existing)
            {
                if (existing.Name != reference.Name)
                {
                    existing.Name = reference.Name;
                    return true;
                }

                return false;
            }
            else
            {
                _collection.Add(reference);
                return true;
            }
        }

        public XElement CreateXML()
        {
            return new XElement("Database",
                                _collection.Select(r => r.CreateXML()));
        }

        public void LoadXML(XElement xml)
        {
            _collection.Clear();
            _collection.UnionWith(xml.Elements().Select(e => ObjectReference.FromXML(e)));
        }
    }
}
