﻿using NPC.Common;
using NPC.Presenter.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPC.Presenter
{
    class Factory: IFactory
    {
        private IStorage _storage;
        private Data.IFactory _factory;

        public Factory(IStorage storage, Data.IFactory factory)
        {
            _storage = storage;
            _factory = factory;
        }

        public IGameObject CreateNew(ObjectType type)
        {
            return _factory.Create(type).CreatePresenter();
        }

        public IGameObject Duplicate(IGameObjectReference reference)
        {
            if (!(reference is IGameObject go))
            {
                go = _storage.Open(reference as IGameObjectMetadata);
            }

            var gameObject = _factory.Create(go.Type).CreatePresenter() as GameObject;
            gameObject.CopyData(go);
            return gameObject;
        }

        public IEnumerable<IGameObject> Duplicate(IEnumerable<IGameObjectReference> references)
        {
            return references.Select(r => Duplicate(r));
        }

        public void CopyTo(IGameObject target, IGameObjectReference source)
        {
            if (!(source is IGameObject go))
            {
                go = _storage.Open(source as IGameObjectMetadata);
            }

            if (target.Type != go.Type)
            {
                throw new ArgumentException("Target and Source are not of the same type.");
            }

            (target as GameObject)?.CopyData(go);
        }
    }
}
