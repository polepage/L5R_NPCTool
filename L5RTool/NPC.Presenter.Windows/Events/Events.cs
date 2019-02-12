using NPC.Presenter.GameObjects;
using Prism.Events;

namespace NPC.Presenter.Windows.Events
{
    class OpenGameObjectEvent: PubSubEvent<IGameObject> { }

    class SaveCurrentGameObjectEvent: SingleSubEvent { }
    class SaveAllGameObjectsEvent: SingleSubEvent { }
}
