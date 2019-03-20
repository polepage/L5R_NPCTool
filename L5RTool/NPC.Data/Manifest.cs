using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CS.Utils.Collections;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class Manifest : IManifest
    {
        private ObservableHashSet<GameObjectMetadata> _collection;

        public Manifest()
        {
            _collection = new ObservableHashSet<GameObjectMetadata>();
        }

        public IEnumerable<IGameObjectMetadata> GameObjects => _collection;

        public bool AddOrModify(GameObjectMetadata metadata)
        {
            if (_collection.FirstOrDefault(or => or.Equals(metadata)) is GameObjectMetadata existing)
            {
                bool modified = false;
                if (existing.Name != metadata.Name)
                {
                    existing.Name = metadata.Name;
                    modified = true;
                }
                if (existing.Keywords.SequenceEqual(metadata.Keywords))
                {
                    existing.UpdateKeywords(metadata.Keywords);
                    modified = true;
                }

                return modified;
            }
            else
            {
                _collection.Add(metadata);
                return true;
            }
        }

        public bool Remove(GameObjectMetadata metadata)
        {
            return _collection.Remove(metadata);
        }

        public XElement CreateXml()
        {
            return new XElement("Database",
                                _collection.Select(m => m.CreateXML()));
        }

        public void LoadXml(XElement xml)
        {
            _collection.Clear();
            _collection.UnionWith(xml.Elements().Select(e => e.LoadMetadata()));
        }
    }
}
