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

        public void Export(IEnumerable<IGameObjectReference> references, string target)
        {
            var gameObjects = _storage.Open(references.OfType<GameObjectMetadata>())
                                .OfType<GameObject>()
                                .Concat(references.OfType<GameObject>());

            SaveExportedFile(new XElement("L5RFile", gameObjects.Select(go => go.CreateXML()).ToArray()), target);
        }

        private void SaveExportedFile(XElement xml, string target)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            xml.Save(target);
        }
    }
}
