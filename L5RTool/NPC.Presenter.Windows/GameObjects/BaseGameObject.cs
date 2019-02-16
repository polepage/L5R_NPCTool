﻿using CS.Utils.Prism.Mvvm;
using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    abstract class BaseGameObject<T> : RelayBindableBase, IGameObject, IGameObjectSource where T : Business.GameObjects.IGameObject
    {
        public BaseGameObject(T gameObject)
            : base(gameObject)
        {
            GameObject = gameObject;
        }

        public ObjectType Type => GameObject.Type;
        public bool IsDirty => GameObject.IsDirty;

        public Business.GameObjects.IGameObject SourceObject => GameObject;

        protected T GameObject { get; }

        public override int GetHashCode()
        {
            return GameObject.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseGameObject<T> bgo)
            {
                return GameObject.Equals(bgo.GameObject);
            }

            return false;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(GameObject.IsDirty), nameof(IsDirty));
        }
    }
}
