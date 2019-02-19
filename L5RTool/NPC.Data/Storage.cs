using System.Collections.Generic;
using System.Linq;
using System.IO;
using NPC.Data.GameObjects;
using System.Xml.Linq;
using System;

namespace NPC.Data
{
    class Storage : IStorage
    {
        private readonly string DatabaseFolder = "Database";
        private readonly string GameObjectFolder = "GameObjects";
        private readonly string DatabaseFile = "References.db";
        private readonly string GameObjectExtension = ".go";

        private ReferenceDatabase _references;

        public Storage()
        {
            _references = new ReferenceDatabase();
            OpenReferences();
        }

        public IReferenceDatabase Database => _references;

        public void Save(IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                SaveGameObject(go);
                SaveReferences(go);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            IEnumerable<GameObject> gos = gameObjects.OfType<GameObject>();
            foreach (GameObject go in gos)
            {
                SaveGameObject(go);
            }

            SaveReferences(gos.ToArray());
        }

        public IGameObject Open(IObjectReference objectReference)
        {
            if (objectReference is ObjectReference reference)
            {
                return OpenFile(Path.Combine(DatabaseFolder, GameObjectFolder, reference.Id + GameObjectExtension));
            }

            throw new ArgumentException("Data.Storage: Cannot Open reference, reference don't exist.");
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IObjectReference> objectReferences)
        {
            return objectReferences.Select(or => Open(or));
        }

        private void SaveGameObject(GameObject gameObject)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, gameObject.Id + GameObjectExtension);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            gameObject.CreateXML().Save(path);
            gameObject.ResetDirty();
        }

        private IGameObject OpenFile(string path)
        {
            return GameObject.FromXML(XElement.Load(path));
        }

        private void SaveReferences(params GameObject[] gameObjects)
        {
            bool needToSave = false;
            foreach(GameObject gameObject in gameObjects)
            {
                needToSave |= _references.AddOrModify(gameObject.CreateReference());
            }

            if (needToSave)
            {
                string path = Path.Combine(DatabaseFolder, DatabaseFile);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                _references.CreateXML().Save(path);
            }
        }

        private void OpenReferences()
        {
            string path = Path.Combine(DatabaseFolder, DatabaseFile);
            if (File.Exists(path))
            {
                _references.LoadXML(XElement.Load(path));
            }
        }
    }
}
