using System.Collections.Generic;
using System.Linq;
using NPC.Business.GameObjects;

namespace NPC.Business
{
    class ExternalStorage : IExternalStorage
    {
        private Data.IExternalStorage _externalStorage;

        public ExternalStorage(Data.IExternalStorage externalStorage)
        {
            _externalStorage = externalStorage;
        }

        public void Export(IEnumerable<IGameObjectReference> references, string target)
        {
            _externalStorage.Export(
                references
                    .OfType<GameObjectReference>()
                    .Select(go => go.ReferenceSource),
                target);
        }
    }
}
