using System;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    class GameObject : GameObjectReference, IGameObject
    {
        public GameObject(Data.GameObjects.IGameObject source)
            : base (source)
        {
            Source = source;
            Data = CreateData(source.Data);
        }

        public GameObject(Data.GameObjects.IGameObject source, IGameObject copySource)
            : this (source)
        {
            Name = copySource.Name;
            CopyData(copySource.Data);
        }

        public Data.GameObjects.IGameObject Source { get; }

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

        private IGameObjectData CreateData(Data.GameObjects.IGameObjectData source)
        {
            switch (source)
            {
                case Data.GameObjects.IDemeanor s:
                    return new Demeanor(s);
                case Data.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Data.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                case Data.GameObjects.IAbility s:
                    return new Ability(s);
                case Data.GameObjects.IGear s:
                    return new Gear(s);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Business: Unknown type.");
                case null:
                    throw new ArgumentNullException("NPC.Business: Data object is null.");
            }
        }

        private void CopyData(IGameObjectData copySource)
        {
            if (Data is ICopyTarget copyTarget)
            {
                copyTarget.CopyData(copySource);
            }
        }
    }
}
