using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private DelegateCommand<Action> _exitingCommand;
        public ICommand ExitingCommand => _exitingCommand ?? (_exitingCommand = new DelegateCommand<Action>(Exiting));

        private void Exiting(Action cancel)
        {
            _eventAggregator.GetEvent<CancellableCloseAllGameObjectsEvent>().Publish(cancel);
        }
    }
}
