using Prism.Interactivity.InteractionRequest;

namespace L5RUI.Interaction
{
    public static class InteractionRequests
    {
        private static InteractionRequest<IConfirmation> _newRequest;
        public static InteractionRequest<IConfirmation> NewRequest => _newRequest ?? (_newRequest = new InteractionRequest<IConfirmation>());

        private static InteractionRequest<INotification> _exitRequest;
        public static InteractionRequest<INotification> ExitRequest => _exitRequest ?? (_exitRequest = new InteractionRequest<INotification>());
    }
}
