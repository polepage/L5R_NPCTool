using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class MainWindowViewModel : BaseDialogViewModel
    {
        private IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ExitApplicationEvent>().Subscribe(Exit);
        }

        private DelegateCommand<Action> _exitingCommand;
        public ICommand ExitingCommand => _exitingCommand ?? (_exitingCommand = new DelegateCommand<Action>(Exiting));

        private void Exiting(Action cancel)
        {
            _eventAggregator.GetEvent<CancellableCloseAllGameObjectsEvent>().Publish(cancel);
        }

        private void Exit()
        {
            RaiseRequestClose(new DialogResult(true));
        }
    }
}
