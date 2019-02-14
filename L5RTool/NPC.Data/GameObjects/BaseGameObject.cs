using Prism.Mvvm;
using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    abstract class BaseGameObject : BindableBase
    {
        public BaseGameObject()
        {
            Id = Guid.NewGuid();
        }

        private bool _isDirty = true;
        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        public Guid Id { get; }

        public abstract XElement GenerateXML();
    }
}
