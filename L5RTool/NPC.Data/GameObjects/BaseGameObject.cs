using Prism.Mvvm;

namespace NPC.Data.GameObjects
{
    abstract class BaseGameObject : BindableBase
    {
        private bool _isDirty = true;
        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }
    }
}
