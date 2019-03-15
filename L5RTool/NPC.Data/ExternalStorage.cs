using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NPC.Data.GameObjects;

namespace NPC.Data
{
    class ExternalStorage : IExternalStorage
    {
        private IStorage _storage;
        
        public ExternalStorage(IStorage storage)
        {
            _storage = storage;
        }

        public void Import(string target)
        {
            var xml = XElement.Load(target);
            var gameObjects = xml.Elements().Select(x => x.LoadGameObject());

            _storage.Save(gameObjects.ToList());
        }

        public void Export(IEnumerable<IGameObjectReference> references, string target)
        {
            var gameObjects = _storage.Open(references.OfType<GameObjectMetadata>())
                                .OfType<GameObject>()
                                .Concat(references.OfType<GameObject>());

            SaveExportedFile(new XElement("L5RFile", gameObjects.Select(go => go.CreateXml(true)).ToArray()), target);
        }

        private void SaveExportedFile(XElement xml, string target)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            xml.Save(target);
        }
    }
}
