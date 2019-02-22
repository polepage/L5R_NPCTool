using System;
using System.ComponentModel;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class GameObject : GameObjectReference, IGameObject
    {
        private GameObjectData _data;

        public GameObject(ObjectType type)
            : this(Guid.NewGuid(), type)
        {
        }

        private GameObject(Guid id, ObjectType type)
            : base(id)
        {
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
                Guid.Parse(xml.Attribute("Id").Value),
                (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value));

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

        public GameObjectMetadata ExtractMetadata()
        {
            return new GameObjectMetadata(Id, Type)
            {
                Name = Name
            };
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
