using NPC.Presenter.GameObjects;
using System.Collections.Generic;

namespace NPC.Presenter
{
    public interface IManifest
    {
        IEnumerable<IGameObjectMetadata> GameObjects { get; }
    }
}
