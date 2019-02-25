using NPC.Presenter.GameObjects;
using Prism.Events;
using System;
using System.Collections.Generic;

namespace NPC.Presenter.Windows.Events
{
    class OpenGameObjectEvent: PubSubEvent<IGameObject> { }

    class SaveCurrentGameObjectEvent: SingleSubEvent { }
    class SaveAllGameObjectsEvent: SingleSubEvent { }

    class CloseCurrentGameObjectEvent: PubSubEvent { }
    class CloseAllGameObjectsEvent: PubSubEvent { }
    class CancellableCloseAllGameObjectsEvent: PubSubEvent<Action> { }
    class ForceCloseGameObjects: PubSubEvent<IEnumerable<IGameObjectReference>> { }

    class DuplicateCurrentGameObjectEvent: SingleSubEvent { }

    class ExportGameObjectsEvent: SingleSubEvent<IEnumerable<IGameObjectMetadata>> { }

    class ExitApplicationEvent: SingleSubEvent { }
}
