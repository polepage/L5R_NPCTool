using System;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace L5RTool.ViewModels
{
    class NewDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private Confirmation _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set => _confirmation = value as Confirmation;
        }

        public Action FinishInteraction { get; set; }
    }
}
