using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    abstract class GameObject : GameObjectReference, IGameObject
    {
        protected GameObject(Data.GameObjects.IGameObject source)
            : base(source)
        {
            Source = source;
        }

        public Data.GameObjects.IGameObject Source { get; }

        public ObjectType Type => Source.Type;
        public bool IsDirty => Source.IsDirty;

        public string Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }

        public virtual void CopyData(IGameObject copySource)
        {
            Name = copySource.Name;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(Source.Name), nameof(Name));
            AddBinding(nameof(Source.IsDirty), nameof(IsDirty));
        }
    }
}
