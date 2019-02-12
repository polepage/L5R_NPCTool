using System;
using System.Windows.Input;
using NPC.Presenter.Windows.Interaction.Notifications;
using NPC.Common;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Collections.Generic;
using CS.Utils;

namespace NPC.Presenter.Windows.ViewModels
{
    class NewDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private DelegateCommand _createCommand;
        public ICommand CreateCommand => _createCommand ?? (_createCommand = new DelegateCommand(Create));

        private ValueConfirmation<ObjectType> _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set
            {
                if (SetProperty(ref _confirmation, value as ValueConfirmation<ObjectType>))
                {
                    Selection = _confirmation.Value;
                }
            }
        }

        public Action FinishInteraction { get; set; }

        public IEnumerable<ObjectType> Types => EnumHelpers.GetValues<ObjectType>();

        private ObjectType _selection;
        public ObjectType Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }

        private void Create()
        {
            _confirmation.Confirmed = true;
            _confirmation.Value = Selection;
            FinishInteraction?.Invoke();
        }
    }
}
