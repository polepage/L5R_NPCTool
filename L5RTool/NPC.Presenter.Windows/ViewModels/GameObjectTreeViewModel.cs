using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class GameObjectTreeViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        private Business.IStorage _storage;
        private Business.IFactory _factory;

        public GameObjectTreeViewModel(IEventAggregator eventAggregator, IDialogService dialogService, Business.IStorage storage, Business.IFactory factory)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _storage = storage;
            _factory = factory;

            GameObjects = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectMetadataGroup(ot, _storage.Database.GameObjects));

            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        public IEnumerable<ObjectMetadataGroup> GameObjects { get; }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public bool IsReferenceSelected => _selectedItems.OfType<GameObjectMetadata>().Any();

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

        private DelegateCommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ?? (_duplicateCommand = new DelegateCommand(Duplicate));

        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        private DelegateCommand _exportCommand;
        public ICommand ExportCommand => _exportCommand ?? (_exportCommand = new DelegateCommand(Export));

        private void Open()
        {
            foreach (IGameObject gameObject in
                _storage.Open(
                        SelectedItems
                            .OfType<GameObjectMetadata>()
                            .Select(or => or.Source))
                        .Select(go => new GameObject(go)))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Duplicate()
        {
            foreach (IGameObject gameObject in
                _factory.Duplicate(
                    SelectedItems
                        .OfType<GameObjectMetadata>()
                        .Select(or => or.Source))
                    .Select(go => new GameObject(go)))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Delete()
        {
            IEnumerable<GameObjectMetadata> references = SelectedItems.OfType<GameObjectMetadata>();
            if (!references.Any())
            {
                return;
            }

            string confirmationContent = " will be deleted permanently.";
            if (references.Count() == 1)
            {
                confirmationContent = "'" + references.First().Name + "'" + confirmationContent;
            }
            else
            {
                confirmationContent = "The selected items" + confirmationContent;
            }

            IDialogParameters parameters = new DialogParameters();
            parameters.Add(Dialog.Title, "L5R NPC Creation Tool");
            parameters.Add(Dialog.Confirmation.Content, confirmationContent);

            _dialogService.ShowDialog(Dialog.Confirmation.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    _eventAggregator.GetEvent<ForceCloseGameObjects>().Publish(references);
                    _storage.Delete(references.Select(r => r.Source));
                }
            });
        }

        private void Export()
        {
            _eventAggregator.GetEvent<ExportGameObjectsEvent>().Publish(SelectedItems.OfType<GameObjectMetadata>());
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsReferenceSelected));
        }
    }
}
