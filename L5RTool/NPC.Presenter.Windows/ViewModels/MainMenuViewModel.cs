using NPC.Common;
using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.Extensions;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class MainMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        private IParser _textParser;
        private IFactory _factory;
        private IStorage _storage;
        private IExternalStorage _externalStorage;

        public MainMenuViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IParser parser, IFactory factory, IStorage storage, IExternalStorage externalStorage)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ExportGameObjectsEvent>().Subscribe(ExportReferences);

            _dialogService = dialogService;
            _textParser = parser;
            _factory = factory;
            _storage = storage;
            _externalStorage = externalStorage;
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

        private DelegateCommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ?? (_duplicateCommand = new DelegateCommand(Duplicate));

        private DelegateCommand _importCommand;
        public ICommand ImportCommand => _importCommand ?? (_importCommand = new DelegateCommand(Import));

        private DelegateCommand _exportCommand;
        public ICommand ExportCommand => _exportCommand ?? (_exportCommand = new DelegateCommand(Export));

        private DelegateCommand _printCommand;
        public ICommand PrintCommand => _printCommand ?? (_printCommand = new DelegateCommand(Print));

        private DelegateCommand _exitCommand;
        public ICommand ExitCommand => _exitCommand ?? (_exitCommand = new DelegateCommand(Exit));

        private DelegateCommand _aboutCommand;
        public ICommand AboutCommand => _aboutCommand ?? (_aboutCommand = new DelegateCommand(About));

        private void New()
        {
            IDialogParameters parameters = new DialogParameters
            {
                { Dialog.Title, "Create New Game Object" }
            };

            _dialogService.ShowDialog(Dialog.New.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    IGameObject gameObject = _factory.CreateNew(dialogResult.Parameters.GetValue<ObjectType>(Dialog.New.Type));
                    if (gameObject != null)
                    {
                        _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
                    }
                }
            });
        }

        private void Open()
        {
            IDialogParameters parameters = new DialogParameters
            {
                { Dialog.Title, "Open Game Object" },
                { Dialog.Selection.Source, _storage.Database },
                { Dialog.Selection.Accept, "Open" }
            };

            _dialogService.ShowDialog(Dialog.Selection.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    foreach (IGameObject gameObject in _storage.Open(dialogResult.Parameters.GetValue<IEnumerable<IGameObjectMetadata>>(Dialog.Selection.SelectedItems)))
                    {
                        _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
                    }
                }
            });
        }

        private void Close()
        {
            _eventAggregator.GetEvent<CloseCurrentGameObjectEvent>().Publish();
        }

        private void CloseAll()
        {
            _eventAggregator.GetEvent<CloseAllGameObjectsEvent>().Publish();
        }

        private void Save()
        {
            _eventAggregator.GetEvent<SaveCurrentGameObjectEvent>().Publish();
        }

        private void SaveAll()
        {
            _eventAggregator.GetEvent<SaveAllGameObjectsEvent>().Publish();
        }

        private void Duplicate()
        {
            _eventAggregator.GetEvent<DuplicateCurrentGameObjectEvent>().Publish();
        }

        private void Import()
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "Select export file" },
                { Dialog.File.Filter, "L5R Files (*.l5r)|*.l5r" }
            };

            _dialogService.ShowOpenDialog(parameters, dialogResult =>
            {
                _externalStorage.Import(dialogResult.Parameters.GetValue<string>(Dialog.File.Target));
            });
        }

        private void Export()
        {
            IDialogParameters parameters = new DialogParameters
            {
                { Dialog.Title, "Export Game Objects" },
                { Dialog.Selection.Source, _storage.Database },
                { Dialog.Selection.Accept, "Export" }
            };

            _dialogService.ShowDialog(Dialog.Selection.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    ExportReferences(dialogResult.Parameters.GetValue<IEnumerable<IGameObjectMetadata>>(Dialog.Selection.SelectedItems));
                }
            });
        }

        private void Print()
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "Print Game Objects" },
                { Dialog.Print.Source, _storage.Database },
                { Dialog.Print.Opener, (Func<IGameObjectReference, IGameObject>)(or => _storage.Open(or)) },
                { Dialog.Print.Parser, _textParser }
            };

            _dialogService.ShowDialog(Dialog.Print.Name, parameters, dr => { });
        }

        private void Exit()
        {
            _eventAggregator.GetEvent<ExitApplicationEvent>().Publish();
        }

        private void About()
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "About" }
            };

            _dialogService.ShowDialog(Dialog.About.Name, parameters, dialogResults => { });
        }

        private void ExportReferences(IEnumerable<IGameObjectReference> selection)
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "Select export file" },
                { Dialog.File.Filter, "L5R Files (*.l5r)|*.l5r" }
            };

            _dialogService.ShowSaveDialog(parameters, dialogResult =>
            {
                _externalStorage.Export(selection, dialogResult.Parameters.GetValue<string>(Dialog.File.Target));
            });
        }
    }
}
