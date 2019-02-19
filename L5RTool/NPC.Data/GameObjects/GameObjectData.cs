using Prism.Mvvm;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    abstract class GameObjectData: BindableBase, IGameObjectData
    {
        private bool _isDirty = true;
        public bool IsDirty
        {
            get => _isDirty;
            protected set => SetProperty(ref _isDirty, value);
        }

        public void ResetDirty()
        {
            IsDirty = false;
        }

        public abstract void LoadXML(XElement xml);
        public abstract XElement CreateXML();
    }
}
