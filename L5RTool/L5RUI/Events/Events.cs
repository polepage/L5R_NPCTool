using NPC;
using Prism.Events;

namespace L5RUI.Events
{
    class OpenNewElementEvent: PubSubEvent<IElement> { }
    class OpenElementEvent: PubSubEvent<IElement> { }

    class SaveCurrentElementEvent: SingleSubEvent { }
    class SaveAllElementsEvent: SingleSubEvent { }
}
