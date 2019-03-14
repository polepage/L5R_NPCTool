using NPC.Presenter.GameObjects;
using System.Collections.Generic;

namespace NPC.Presenter
{
    public interface IExternalStorage
    {
        void Import(string target);
        void Export(IEnumerable<IGameObjectReference> references, string target);
    }
}
