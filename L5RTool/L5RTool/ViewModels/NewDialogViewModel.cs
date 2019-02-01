﻿using System;
using System.Collections;
using System.Windows.Input;
using L5RTool.Elements;
using L5RTool.Interaction.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace L5RTool.ViewModels
{
    class NewDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private DelegateCommand _createCommand;
        public ICommand CreateCommand => _createCommand ?? (_createCommand = new DelegateCommand(Create));

        private ValueConfirmation<ElementType> _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set
            {
                if (SetProperty(ref _confirmation, value as ValueConfirmation<ElementType>))
                {
                    Selection = _confirmation.Value;
                }
            }
        }

        public Action FinishInteraction { get; set; }

        public IEnumerable Types => Enum.GetValues(typeof(ElementType));

        private ElementType _selection;
        public ElementType Selection
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