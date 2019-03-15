using System;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class GameObjectMetadata : GameObjectReference, IGameObjectMetadata
    {
        public GameObjectMetadata(Guid id, ObjectType type)
            : base(id)
        {
            Type = type;
        }

        public ObjectType Type { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public static GameObjectMetadata FromXml(XElement xml)
        {
            return new GameObjectMetadata(Guid.Parse(xml.Attribute("Id").Value),
                                          (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value))
            {
                Name = xml.Value.Replace("\n", Environment.NewLine)
            };
        }

        public XElement CreateXML()
        {
            return new XElement(XmlTools.MetadataNode,
                                new XAttribute("Type", Type),
                                new XAttribute("Id", Id),
                                Name);
        }
    }
}
