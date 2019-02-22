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

        private Database _database;

        public Storage()
        {
            _database = new Database();
            OpenDatabase();
        }

        public IDatabase Database => _database;

        public void Save(IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                SaveGameObject(go);
                SaveDatabase(go);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            IEnumerable<GameObject> gos = gameObjects.OfType<GameObject>();
            foreach (GameObject go in gos)
            {
                SaveGameObject(go);
            }

            SaveDatabase(gos.ToArray());
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

        private void SaveDatabase(params GameObject[] gameObjects)
        {
            bool needToSave = false;
            foreach(GameObject gameObject in gameObjects)
            {
                needToSave |= _database.AddOrModify(gameObject.ExtractMetadata());
            }

            if (needToSave)
            {
                string path = Path.Combine(DatabaseFolder, DatabaseFile);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                _database.CreateXML().Save(path);
            }
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
