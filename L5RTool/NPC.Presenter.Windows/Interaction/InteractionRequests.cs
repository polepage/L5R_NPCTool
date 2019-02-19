using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Interaction.Notifications;
using Prism.Interactivity.InteractionRequest;
using System.Collections.Generic;

namespace NPC.Presenter.Windows.Interaction
{
    static class InteractionRequests
    {
        private static InteractionRequest<ValueConfirmation<ObjectType>> _newRequest;
        public static InteractionRequest<ValueConfirmation<ObjectType>> NewRequest => _newRequest ?? (_newRequest = new InteractionRequest<ValueConfirmation<ObjectType>>());

        private static InteractionRequest<ValueConfirmation<IEnumerable<IObjectReference>>> _openRequest;
        public static InteractionRequest<ValueConfirmation<IEnumerable<IObjectReference>>> OpenRequest => _openRequest ?? (_openRequest = new InteractionRequest<ValueConfirmation<IEnumerable<IObjectReference>>>());

        private static InteractionRequest<INotification> _exitRequest;
        public static InteractionRequest<INotification> ExitRequest => _exitRequest ?? (_exitRequest = new InteractionRequest<INotification>());
    }
}
