using System;
using System.Xml.Linq;
using NPC.Common;
using Prism.Mvvm;

namespace NPC.Data.GameObjects
{
    class ObjectReference : BindableBase, IObjectReference
    {
        public ObjectReference(Guid id, ObjectType type)
        {
            Id = id;
            Type = type;
        }

        public Guid Id { get; }
        public ObjectType Type { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public static ObjectReference FromXML(XElement xml)
        {
            return new ObjectReference(Guid.Parse(xml.Attribute("Id").Value),
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

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectReference or)
            {
                return Id == or.Id;
            }

            return false;
        }
    }
}
