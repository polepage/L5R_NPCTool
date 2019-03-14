using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    class GameObjectMetadata : GameObjectReference, IGameObjectMetadata
    {
        public GameObjectMetadata(Data.GameObjects.IGameObjectMetadata source)
            : base(source)
        {
            Source = source;
        }

        public Data.GameObjects.IGameObjectMetadata Source { get; }

        public ObjectType Type => Source.Type;
        public string Name => Source.Name;

        protected override void RegisterBindings()
        {
            AddBinding(nameof(Source.Type), nameof(Type));
            AddBinding(nameof(Source.Name), nameof(Name));
        }
    }
}
