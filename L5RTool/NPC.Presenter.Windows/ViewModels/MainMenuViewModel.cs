using NPC.Common;
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
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class MainMenuViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        private Business.IFactory _factory;
        private Business.IStorage _storage;
        private Business.IExternalStorage _externalStorage;

        public MainMenuViewModel(IEventAggregator eventAggregator, IDialogService dialogService, Business.IFactory factory, Business.IStorage storage, Business.IExternalStorage externalStorage)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ExportGameObjectsEvent>().Subscribe(ExportReferences);

            _dialogService = dialogService;
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

        private void New()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Create New Game Object");

            _dialogService.ShowDialog(Dialog.New.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    IGameObject gameObject = new GameObject(
                        _factory.CreateNew(dialogResult.Parameters.GetValue<ObjectType>(Dialog.New.Type)));
                    if (gameObject != null)
                    {
                        _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
                    }
                }
            });
        }

        private void Open()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Open Game Object");
            parameters.Add(Dialog.Selection.Source, _storage.Database);
            parameters.Add(Dialog.Selection.Accept, "Open");

            _dialogService.ShowDialog(Dialog.Selection.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    foreach (IGameObject gameObject in
                        _storage.Open(
                            dialogResult.Parameters.GetValue<IEnumerable<IGameObjectMetadata>>(Dialog.Selection.SelectedItems)
                                .Select(or => or.GetSource()))
                            .Select(go => new GameObject(go)))
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
            var parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Select export file");
            parameters.Add(Dialog.File.Filter, "L5R Files (*.l5r)|*.l5r");

            _dialogService.ShowOpenDialog(parameters, dialogResult =>
            {
                _externalStorage.Import(dialogResult.Parameters.GetValue<string>(Dialog.File.Target));
            });
        }

        private void Export()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Export Game Objects");
            parameters.Add(Dialog.Selection.Source, _storage.Database);
            parameters.Add(Dialog.Selection.Accept, "Export");

            _dialogService.ShowDialog(Dialog.Selection.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    ExportReferences(dialogResult.Parameters.GetValue<IEnumerable<GameObjectMetadata>>(Dialog.Selection.SelectedItems));
                }
            });
        }

        private void Print()
        {
            var parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Print Game Objects");
            parameters.Add(Dialog.Print.Source, _storage.Database);
            parameters.Add(Dialog.Print.Opener, (Func<IGameObjectReference, IGameObject>)(or => new GameObject(_storage.Open(or.GetSource()))));

            _dialogService.ShowDialog(Dialog.Print.Name, parameters, dr => { });
        }

        private void Exit()
        {
            _eventAggregator.GetEvent<ExitApplicationEvent>().Publish();
        }

        private void ExportReferences(IEnumerable<IGameObjectReference> selection)
        {
            var parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "Select export file");
            parameters.Add(Dialog.File.Filter, "L5R Files (*.l5r)|*.l5r");

            _dialogService.ShowSaveDialog(parameters, dialogResult =>
            {
                _externalStorage.Export(selection
                        .Select(go => go.GetSource()),
                    dialogResult.Parameters.GetValue<string>(Dialog.File.Target));
            });
        }
    }
}
