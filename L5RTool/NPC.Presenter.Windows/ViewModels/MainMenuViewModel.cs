using NPC.Business;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.GameObjects;
using NPC.Presenter.Windows.Interaction;
using NPC.Presenter.Windows.Interaction.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class MainMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggegator;
        private IGameObjectFactory _gameObjectFactory;

        private InternalFactory _factory;

        public MainMenuViewModel(IEventAggregator eventAggregator, IGameObjectFactory gameObjectFactory, InternalFactory internalFactory)
        {
            _eventAggegator = eventAggregator;
            _gameObjectFactory = gameObjectFactory;

            _factory = internalFactory;
        }

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

        private void New()
        {
            var confirmation = new ValueConfirmation<ObjectType>
            {
                Title = "Create New Game Object",
                Value = 0
            };

            InteractionRequests.NewRequest.Raise(confirmation);
            if (confirmation.Confirmed)
            {
                IGameObject gameObject = _factory.Create(_gameObjectFactory.CreateNewObject(confirmation.Value));
                if (gameObject != null)
                {
                    _eventAggegator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
                }
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
            _eventAggegator.GetEvent<SaveCurrentGameObjectEvent>().Publish();
        }

        private void SaveAll()
        {
            _eventAggegator.GetEvent<SaveAllGameObjectsEvent>().Publish();
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
            //if (IsDirty)
            //{
            //    // Pop Dirty Dialog
            //}

            InteractionRequests.ExitRequest.Raise(new Notification());
        }
    }
}
