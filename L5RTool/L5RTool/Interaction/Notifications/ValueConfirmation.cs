using Prism.Interactivity.InteractionRequest;

namespace L5RTool.Interaction.Notifications
{
    class ValueConfirmation<T>: Confirmation
    {
        public T Value { get; set; }
    }
}
