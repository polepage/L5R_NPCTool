using System.Collections.Generic;
using System.Linq;
using System.IO;
using NPC.Data.GameObjects;
using System.Xml.Linq;

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
            var baseObject = gameObject as BaseGameObject;

            SaveGameObject(baseObject);
            SaveReferences(baseObject);
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            IEnumerable<BaseGameObject> baseGameObjects = gameObjects.OfType<BaseGameObject>();
            foreach (BaseGameObject baseGameObject in baseGameObjects)
            {
                SaveGameObject(baseGameObject);
            }

            SaveReferences(baseGameObjects.ToArray());
        }

        private void SaveGameObject(BaseGameObject baseObject)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, baseObject.Id + GameObjectExtension);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            baseObject.GenerateXML().Save(path);

            baseObject.IsDirty = false;
        }

        private void SaveReferences(params BaseGameObject[] baseGameObjects)
        {
            bool needToSave = false;
            foreach(BaseGameObject baseGameObject in  baseGameObjects)
            {
                needToSave |= _references.AddOrModify(baseGameObject.CreateReference());
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
