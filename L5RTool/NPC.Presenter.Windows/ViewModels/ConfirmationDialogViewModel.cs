using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class ConfirmationDialogViewModel : BaseDialogViewModel
    {
        private DelegateCommand _acceptCommand;
        public ICommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new DelegateCommand(() => RaiseRequestClose(new DialogResult(true))));

        private string _content;
        public string Content
        {
            get => _content;
            private set => SetProperty(ref _content, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);
            Content = parameters.GetValue<string>(Dialog.Confirmation.Content);
        }
    }
}
