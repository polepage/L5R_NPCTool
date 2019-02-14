using System.Collections.Generic;
using System.IO;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class Storage : IStorage
    {
        private readonly string DatabaseFolder = "Database";
        private readonly string GameObjectFolder = "GameObjects";

        public void Save(IGameObject gameObject)
        {
            var baseObject = gameObject as BaseGameObject;

            SaveGameObject(baseObject);

            baseObject.IsDirty = false;
        }

        public void Save(IEnumerable<IGameObject> gameObjects)
        {
            foreach(IGameObject gameObject in gameObjects)
            {
                Save(gameObject);
            }
        }

        private void SaveGameObject(BaseGameObject baseObject)
        {
            string path = Path.Combine(DatabaseFolder, GameObjectFolder, baseObject.Id + ".go");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            baseObject.GenerateXML().Save(path);
        }
    }
}
