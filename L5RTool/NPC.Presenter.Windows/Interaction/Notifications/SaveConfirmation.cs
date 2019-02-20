using NPC.Presenter.GameObjects;
using System;

namespace NPC.Presenter.Windows.Interaction.Notifications
{
    class SaveConfirmation: ValueConfirmation<bool>
    {
        public Action<IGameObject> Selector { get; set; }
    }
}
