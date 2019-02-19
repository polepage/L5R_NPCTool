﻿using System;
using CS.Utils.Prism.Mvvm;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    class GameObject : RelayBindableBase, IGameObject
    {
        public GameObject(Data.GameObjects.IGameObject source)
            : base (source)
        {
            Source = source;
            Data = CreateData(source.Data);
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

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObject go)
            {
                return Source.Equals(go.Source);
            }

            return false;
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
                case Data.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Data.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Business: Unknown type.");
                case null:
                    throw new ArgumentNullException("NPC.Business: Data object is null.");
            }
        }
    }
}