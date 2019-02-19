using System;
using System.ComponentModel;
using System.Xml.Linq;
using NPC.Common;
using Prism.Mvvm;

namespace NPC.Data.GameObjects
{
    class GameObject : BindableBase, IGameObject
    {
        private GameObjectData _data;

        public GameObject(ObjectType type)
            : this(type, Guid.NewGuid())
        {
        }

        private GameObject(ObjectType type, Guid id)
        {
            Id = id;
            Type = type;
            _data = CreateData(type);

            _data.PropertyChanged += IsDirtyChanged;
        }

        private bool _isDirty = true;
        public bool IsDirty
        {
            get => _isDirty | _data.IsDirty;
            private set => SetProperty(ref _isDirty, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => IsDirty |= SetProperty(ref _name, value);
        }

        public Guid Id { get; }
        public ObjectType Type { get; }
        public IGameObjectData Data => _data;

        public void ResetDirty()
        {
            IsDirty = false;
            _data.ResetDirty();
        }

        public static GameObject FromXML(XElement xml)
        {
            var gameObject = new GameObject(
                (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value),
                Guid.Parse(xml.Attribute("Id").Value));

            gameObject.Name = xml.Element("Name").Value;
            gameObject._data.LoadXML(xml);

            gameObject.ResetDirty();

            return gameObject;
        }

        public XElement CreateXML()
        {
            return new XElement("GameObject",
                                new XAttribute("Id", Id),
                                new XAttribute("Type", Type),
                                new XElement("Name", Name),
                                _data.CreateXML());
        }

        public ObjectReference CreateReference()
        {
            return new ObjectReference(Id, Type)
            {
                Name = Name
            };
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObject go)
            {
                return Id == go.Id;
            }

            return false;
        }

        private void IsDirtyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_data.IsDirty))
            {
                RaisePropertyChanged(nameof(IsDirty));
            }
        }

        private GameObjectData CreateData(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Advantage:
                    return new Advantage();
                case ObjectType.Disadvantage:
                    return new Disadvantage();
                default:
                    throw new ArgumentOutOfRangeException("NPC.Data: Unknown object type.");
            }
        }
    }
}
