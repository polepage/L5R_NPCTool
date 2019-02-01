using Prism.Interactivity.InteractionRequest;

namespace L5RUI.Interaction.Notifications
{
    class ValueConfirmation<T>: Confirmation
    {
        public T Value { get; set; }
    }
}
