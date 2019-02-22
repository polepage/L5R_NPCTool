using NPC.Presenter.GameObjects;
using Prism.Events;
using System;

namespace NPC.Presenter.Windows.Events
{
    class OpenGameObjectEvent: PubSubEvent<IGameObject> { }

    class SaveCurrentGameObjectEvent: SingleSubEvent { }
    class SaveAllGameObjectsEvent: SingleSubEvent { }

    class CloseCurrentGameObjectEvent: PubSubEvent { }
    class CloseAllGameObjectsEvent: PubSubEvent { }
    class CancellableCloseAllGameObjectsEvent: PubSubEvent<Action> { }

    class DuplicateCurrentGameObjectEvent: SingleSubEvent { }

    class ExitApplicationEvent: SingleSubEvent { }
}
