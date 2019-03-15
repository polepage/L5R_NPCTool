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

                if (UpdateDatabase(go))
                {
                    SaveDatabase();
                }
            }
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            bool needToSaveDB = false;
            foreach (GameObject go in gameObjects)
            {
                ValidateName(go);
                SaveGameObject(go);
                needToSaveDB |= UpdateDatabase(go);
            }

            if (needToSaveDB)
            {
                SaveDatabase();
            }
        }

        public IGameObject Open(IGameObjectReference reference)
        {
            if (reference is GameObjectReference go)
            {
                return OpenFile(Path.Combine(DatabaseFolder, GameObjectFolder, go.Id + GameObjectExtension));
            }

            throw new ArgumentException("Data.Storage: Cannot Open metadata, reference don't exist.");
        }

        public IEnumerable<IGameObject> Open(IEnumerable<IGameObjectReference> references)
        {
            return references.Select(go => Open(go));
        }

        public void Delete(IGameObjectReference reference)
        {
            if (reference is GameObjectReference go)
            {
                DeleteGameObject(go);
                if (RemoveFromDatabase(go))
                {
                    SaveDatabase();
                }
            }
        }

        public void Delete(IEnumerable<IGameObjectReference> references)
        {
            bool needToSaveDB = false;
            var toRemove = new List<GameObjectReference>();
            foreach (GameObjectReference go in references)
            {
                DeleteGameObject(go);
                toRemove.Add(go);
            }

            foreach (GameObjectReference go in toRemove)
            {
                needToSaveDB |= RemoveFromDatabase(go);
            }

            if (needToSaveDB)
            {
                SaveDatabase();
            }
        }

        private void SaveGameObject(GameObject gameObject)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, gameObject.Id + GameObjectExtension);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            gameObject.CreateXml().Save(path);
            gameObject.ResetDirty();
        }

        private GameObject OpenFile(string path)
        {
            return XElement.Load(path).LoadGameObject();
        }

        private void DeleteGameObject(GameObjectReference reference)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, reference.Id + GameObjectExtension);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private bool UpdateDatabase(GameObject gameObject)
        {
            return _database.AddOrModify(gameObject.ExtractMetadata());
        }

        private bool RemoveFromDatabase(GameObjectReference gameObject)
        {
            if (gameObject is GameObjectMetadata meta)
            {
                return _database.Remove(meta);
            }
            else if (gameObject is GameObject go)
            {
                return _database.Remove(go.ExtractMetadata());
            }

            throw new ArgumentException("Reference in not a GameObject nor a GameObjectMetadata.");
        }

        private void SaveDatabase()
        {
            string path = Path.Combine(DatabaseFolder, DatabaseFile);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            _database.CreateXml().Save(path);
        }

        private void OpenDatabase()
        {
            string path = Path.Combine(DatabaseFolder, DatabaseFile);
            if (File.Exists(path))
            {
                _database.LoadXml(XElement.Load(path));
            }
        }

        private void ValidateName(GameObject gameObject)
        {
            if (string.IsNullOrEmpty(gameObject.Name))
            {
                gameObject.Name = gameObject.Type.ToString();
            }

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
