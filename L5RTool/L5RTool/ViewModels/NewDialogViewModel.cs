using System;
using System.Collections;
using System.Windows.Input;
using L5RTool.Elements;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace L5RTool.ViewModels
{
    class NewDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private DelegateCommand _createCommand;
        public ICommand CreateCommand => _createCommand ?? (_createCommand = new DelegateCommand(Create));

        private Confirmation _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set
            {
                if (SetProperty(ref _confirmation, value as Confirmation))
                {
                    Selection = 0;
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
            _confirmation.Content = Selection;
            FinishInteraction?.Invoke();
        }
    }
}
