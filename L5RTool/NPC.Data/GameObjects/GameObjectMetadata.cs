using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class GameObjectMetadata : GameObjectReference, IGameObjectMetadata
    {
        private ObservableCollection<string> _keywords;

        public GameObjectMetadata(Guid id, ObjectType type, IEnumerable<string> keywords)
            : base(id)
        {
            Type = type;
            _keywords = new ObservableCollection<string>(keywords);
        }

        public ObjectType Type { get; }
        public IEnumerable<string> Keywords => _keywords;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public static GameObjectMetadata FromXml(XElement xml)
        {
            return new GameObjectMetadata(Guid.Parse(xml.Attribute("Id").Value),
                                          (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value),
                                          xml.Element("Keywords").Elements().Select(e => e.Value))
            {
                Name = xml.Element("Name").Value.Replace("\n", Environment.NewLine)
            };
        }

        public XElement CreateXML()
        {
            return new XElement(XmlTools.MetadataNode,
                                new XAttribute("Type", Type),
                                new XAttribute("Id", Id),
                                new XElement("Name", Name),
                                new XElement("Keywords", Keywords.Select(k => new XElement("Keyword", k)).ToArray()));
        }

        public void UpdateKeywords(IEnumerable<string> newElements)
        {
            foreach (string keyword in _keywords.Where(k => !newElements.Contains(k)).ToList())
            {
                _keywords.Remove(keyword);
            }

            foreach (string keyword in newElements.Where(k => !_keywords.Contains(k)))
            {
                _keywords.Add(keyword);
            }
        }
    }
}
