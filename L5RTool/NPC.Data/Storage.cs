using System.Collections.Generic;
using System.Linq;
using System.IO;
using NPC.Data.GameObjects;
using System.Xml.Linq;
using System;
using System.Text.RegularExpressions;

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

        public IManifest Manifest => _database;

        public void Save(IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                ValidateName(go);
                SaveGameObject(go);
                UpdateDatabase(go);
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            foreach (IGameObject go in gameObjects)
            {
                Save(go);
            }
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

        private void ValidateName(GameObject gameObject)
        {
            if (_database.GameObjects
                    .Where(m => !m.Equals(gameObject))
                    .Where(m => m.Type == gameObject.Type)
                    .Select(m => m.Name)
                    .Contains(gameObject.Name))
            {
                var similarNames = _database.GameObjects
                                        .Where(m => m.Type == gameObject.Type)
                                        .Where(m => AreNamesSimilar(m.Name, gameObject.Name))
                                        .Select(m => m.Name);

                gameObject.Name = CreateUniqueName(gameObject.Name, similarNames);
            }
        }

        private bool AreNamesSimilar(string name1, string name2)
        {
            Match regex1 = Regex.Match(name1, @"(.+) \((\d+)\)");
            Match regex2 = Regex.Match(name2, @"(.+) \((\d+)\)");

            string value1 = regex1.Success ? regex1.Groups[1].Value : name1;
            string value2 = regex2.Success ? regex2.Groups[1].Value : name2;

            return value1 == value2;
        }

        private string CreateUniqueName(string name, IEnumerable<string> similarNames)
        {
            int max = similarNames
                        .Select(n => GetSimilarNameNumber(n))
                        .Max();

            Match regex = Regex.Match(name, @"(.+) \((\d+)\)");
            string baseName = regex.Success ? regex.Groups[1].Value : name;

            return $"{baseName} ({max + 1})";
        }

        private int GetSimilarNameNumber(string name)
        {
            Match regex = Regex.Match(name, @"(.+) \((\d+)\)");
            if (regex.Success)
            {
                return int.Parse(regex.Groups[2].Value);
            }

            return 0;
        }
    }
}
