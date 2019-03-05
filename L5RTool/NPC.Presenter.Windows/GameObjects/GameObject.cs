using NPC.Common;
using System;

namespace NPC.Presenter.GameObjects
{
    class GameObject : GameObjectReference, IGameObject
    {
        public GameObject(Business.GameObjects.IGameObject source)
            : base(source)
        {
            Source = source;
            Data = CreateData(source.Data);
        }

        public Business.GameObjects.IGameObject Source { get; }

        public IGameObjectData Data { get; }

        public ObjectType Type => Source.Type;
        public bool IsDirty => Source.IsDirty;

        public string Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(Source.Name), nameof(Name));
            AddBinding(nameof(Source.IsDirty), nameof(IsDirty));
        }

        private IGameObjectData CreateData(Business.GameObjects.IGameObjectData source)
        {
            switch (source)
            {
                case Business.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Business.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                case Business.GameObjects.IGear s:
                    return new Gear(s);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Presenter: Unknown type.");
                case null:
                    throw new ArgumentNullException("NPC.Presenter: Data object is null.");
            }
        }
    }
}
