using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CS.Utils.Collections;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class Database : IDatabase
    {
        private ObservableHashSet<GameObjectMetadata> _collection;

        public Database()
        {
            _collection = new ObservableHashSet<GameObjectMetadata>();
        }

        public IEnumerable<IGameObjectMetadata> GameObjects => _collection;

        public bool AddOrModify(GameObjectMetadata metadata)
        {
            if (_collection.FirstOrDefault(or => or.Equals(metadata)) is GameObjectMetadata existing)
            {
                if (existing.Name != metadata.Name)
                {
                    existing.Name = metadata.Name;
                    return true;
                }

                return false;
            }
            else
            {
                _collection.Add(metadata);
                return true;
            }
        }

        public XElement CreateXML()
        {
            return new XElement("Database",
                                _collection.Select(m => m.CreateXML()));
        }

        public void LoadXML(XElement xml)
        {
            _collection.Clear();
            _collection.UnionWith(xml.Elements().Select(e => GameObjectMetadata.FromXML(e)));
        }
    }
}
