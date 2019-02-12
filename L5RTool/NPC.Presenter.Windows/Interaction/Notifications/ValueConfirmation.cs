using Prism.Interactivity.InteractionRequest;

namespace NPC.Presenter.Windows.Interaction.Notifications
{
    class ValueConfirmation<T>: Confirmation
    {
        public T Value { get; set; }
    }
}
