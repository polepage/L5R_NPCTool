using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    abstract class GameObject : GameObjectReference, IGameObject
    {
        protected GameObject(ObjectType type)
            : this(Guid.NewGuid(), type)
        {
        }

        protected GameObject(Guid id, ObjectType type)
            : base(id)
        {
            Type = type;
            Name = type.ToString();
        }

        private bool _isDirty = true;
        public bool IsDirty
        {
            get => _isDirty;
            protected set => SetProperty(ref _isDirty, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => IsDirty |= SetProperty(ref _name, value);
        }

        public ObjectType Type { get; }

        public void ResetDirty()
        {
            IsDirty = false;
        }

        public virtual XElement CreateXml(bool external = false)
        {
            var xml = new XElement(XmlTools.GameObjectNode);
            if (!external)
            {
                xml.Add(new XAttribute("Id", Id));
            }

            xml.Add(new XAttribute("Type", Type),
                    new XElement("Name", Name));

            return xml;
        }

        public GameObjectMetadata ExtractMetadata()
        {
            return new GameObjectMetadata(Id, Type, ExtractKeywords())
            {
                Name = Name
            };
        }

        protected static GameObject FromXml(XElement xml, Func<(Guid? id, ObjectType type), GameObject> creator)
        {
            var idElement = xml.Attribute("Id");
            Guid? id = null;
            if (idElement != null)
            {
                id = Guid.Parse(idElement.Value);
            }

            var type = (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value);

            var gameObject = creator((id, type));
            gameObject.LoadXml(xml);

            gameObject.ResetDirty();

            return gameObject;
        }

        protected virtual void LoadXml(XElement xml)
        {
            Name = xml.Element("Name").Value.Replace("\n", Environment.NewLine);
        }

        protected virtual IEnumerable<string> ExtractKeywords()
        {
            return new List<string>();
        }
    }
}
