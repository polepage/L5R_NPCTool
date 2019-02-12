﻿using CS.Utils.Prism.Mvvm;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    abstract class BaseGameObject<T>: RelayBindableBase, IGameObject where T: Data.GameObjects.IGameObject 
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
