using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Windows.Input;

namespace L5RTool.ViewModels
{
    class MainWindowViewModel: BindableBase
    {
        private InteractionRequest<IConfirmation> _newRequest;
        public IInteractionRequest NewRequest => _newRequest ?? (_newRequest = new InteractionRequest<IConfirmation>());

        private InteractionRequest<INotification> _exitRequest;
        public IInteractionRequest ExitRequest => _exitRequest ?? (_exitRequest = new InteractionRequest<INotification>());

        private DelegateCommand _newCommand;
        public ICommand NewCommand => _newCommand ?? (_newCommand = new DelegateCommand(New));

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

        private DelegateCommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private DelegateCommand _closeAllCommand;
        public ICommand CloseAllCommand => _closeAllCommand ?? (_closeAllCommand = new DelegateCommand(CloseAll));

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));

        private DelegateCommand _saveAllCommand;
        public ICommand SaveAllCommand => _saveAllCommand ?? (_saveAllCommand = new DelegateCommand(SaveAll));

        private DelegateCommand _importCommand;
        public ICommand ImportCommand => _importCommand ?? (_importCommand = new DelegateCommand(Import));

        private DelegateCommand _exportCommand;
        public ICommand ExportCommand => _exportCommand ?? (_exportCommand = new DelegateCommand(Export));

        private DelegateCommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new DelegateCommand(Print));

        private DelegateCommand _exitCommand;
        public ICommand ExitCommand => _exitCommand ?? (_exitCommand = new DelegateCommand(Exit));

        public bool IsDirty => false;

        private void New()
        {
            var confirmation = new Confirmation
            {
                Title = "Create New Element"
            };

            _newRequest.Raise(confirmation);
            if (confirmation.Confirmed)
            {
                // Create new data
            }
        }

        private void Open()
        {

        }

        private void Close()
        {

        }

        private void CloseAll()
        {

        }

        private void Save()
        {

        }

        private void SaveAll()
        {

        }

        private void Import()
        {

        }

        private void Export()
        {

        }

        private void Print()
        {

        }

        private void Exit()
        {
            if (IsDirty)
            {
                // Pop Dirty Dialog
            }

            _exitRequest.Raise(new Notification());
        }
    }
}
