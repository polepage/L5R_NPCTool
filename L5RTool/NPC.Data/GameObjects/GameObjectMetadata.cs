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

        public static GameObjectMetadata FromXML(XElement xml)
        {
            return new GameObjectMetadata(Guid.Parse(xml.Attribute("Id").Value),
                                          (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value))
            {
                Name = xml.Value
            };
        }

        public XElement CreateXML()
        {
            return new XElement("Reference",
                                new XAttribute("Type", Type),
                                new XAttribute("Id", Id),
                                Name);
        }
    }
}
