using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;

namespace NPC.Presenter
{
    class ExternalStorage: IExternalStorage
    {
        private Data.IExternalStorage _externalStorage;

        public ExternalStorage(Data.IExternalStorage externalStorage)
        {
            _externalStorage = externalStorage;
        }

        public void Import(string target)
        {
            _externalStorage.Import(target);
        }

        public void Export(IEnumerable<IGameObjectReference> references, string target)
        {
            _externalStorage.Export(
                references
                    .Select(go => go.GetSource()),
                target);
        }
    }
}
