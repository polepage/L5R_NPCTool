using CS.Utils.Prism.Mvvm;
using NPC.Common;
using NPC.Presenter.GameObjects;

namespace NPC.Presenter.Windows.GameObjects
{
    abstract class BaseGameObject<T> : RelayBindableBase, IGameObject where T : Business.GameObjects.IGameObject
    {
        public BaseGameObject(T gameObject)
            : base(gameObject)
        {
            GameObject = gameObject;
        }

        public ObjectType Type => GameObject.Type;
        public bool IsDirty => GameObject.IsDirty;

        protected T GameObject { get; }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(GameObject.IsDirty), nameof(IsDirty));
        }
    }
}
