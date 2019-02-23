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

        private Manifest _database;

        public Storage()
        {
            _database = new Manifest();
            OpenDatabase();
        }

        public IManifest Database => _database;

        public void Save(IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                SaveGameObject(go);
                UpdateDatabase(go);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            IEnumerable<GameObject> gos = gameObjects.OfType<GameObject>();
            foreach (GameObject go in gos)
            {
                SaveGameObject(go);
            }

            UpdateDatabase(gos.ToArray());
        }

        public IGameObject Open(IGameObjectMetadata metadata)
        {
            if (metadata is GameObjectMetadata go)
            {
                return OpenFile(Path.Combine(DatabaseFolder, GameObjectFolder, go.Id + GameObjectExtension));
            }

            throw new ArgumentException("Data.Storage: Cannot Open metadata, reference don't exist.");
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IGameObjectMetadata> metadata)
        {
            return metadata.Select(go => Open(go));
        }

        public void Delete(IGameObjectMetadata metadata)
        {
            if (metadata is GameObjectMetadata go)
            {
                DeleteGameObject(go);
                RemoveFromDatabase(go);
            }
        }

        public void Delete(IEnumerable<IGameObjectMetadata> metadata)
        {
            IEnumerable<GameObjectMetadata> gameObjects = metadata.OfType<GameObjectMetadata>();
            foreach (GameObjectMetadata go in gameObjects)
            {
                DeleteGameObject(go);
            }

            RemoveFromDatabase(gameObjects.ToArray());
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

        private void DeleteGameObject(GameObjectMetadata metadata)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, metadata.Id + GameObjectExtension);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void UpdateDatabase(params GameObject[] gameObjects)
        {
            bool needToSave = false;
            foreach(GameObject gameObject in gameObjects)
            {
                needToSave |= _database.AddOrModify(gameObject.ExtractMetadata());
            }

            if (needToSave)
            {
                SaveDatabase();
            }
        }

        private void RemoveFromDatabase(params GameObjectMetadata[] gameObjects)
        {
            bool needToSave = false;
            foreach(GameObjectMetadata metadata in gameObjects)
            {
                needToSave |= _database.Remove(metadata);
            }

            if (needToSave)
            {
                SaveDatabase();
            }
        }

        private void SaveDatabase()
        {
            string path = Path.Combine(DatabaseFolder, DatabaseFile);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            _database.CreateXML().Save(path);
        }

        private void OpenDatabase()
        {
            string path = Path.Combine(DatabaseFolder, DatabaseFile);
            if (File.Exists(path))
            {
                _database.LoadXML(XElement.Load(path));
            }
        }
    }
}
