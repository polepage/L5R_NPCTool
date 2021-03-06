﻿using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;

namespace NPC.Presenter
{
    class Storage: IStorage
    {
        private Data.IStorage _storage;

        public Storage(Data.IStorage storage)
        {
            _storage = storage;
            Database = new Manifest(storage.Manifest);
        }

        public IManifest Database { get; }

        public void Save(IGameObject gameObject)
        {
            _storage.Save(gameObject.GetSource());
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            _storage.Save(gameObjects.Select(s => s.GetSource()));
        }

        public IGameObject Open(IGameObjectReference reference)
        {
            return _storage.Open(reference.GetSource()).CreatePresenter();
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IGameObjectReference> references)
        {
            return references.Select(go => Open(go));
        }

        public void Delete(IGameObjectReference reference)
        {
            _storage.Delete(reference.GetSource());
        }

        public void Delete(IEnumerable<IGameObjectReference> references)
        {
            _storage.Delete(references.Select(go => go.GetSource()));
        }
    }
}
