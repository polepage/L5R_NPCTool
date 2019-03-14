using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.Extensions;
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
        private IStorage _storage;
        private IFactory _factory;

        public GameObjectTreeViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IStorage storage, IFactory factory)
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

        public bool IsReferenceSelected => _selectedItems.OfType<IGameObjectMetadata>().Any();

        private DelegateCommand _selectCommand;
        public ICommand SelectCommand => _selectCommand ?? (_selectCommand = new DelegateCommand(Open));

        private DelegateCommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ?? (_duplicateCommand = new DelegateCommand(Duplicate));

        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        private DelegateCommand _exportCommand;
        public ICommand ExportCommand => _exportCommand ?? (_exportCommand = new DelegateCommand(Export));

        private void Open()
        {
            foreach (IGameObject gameObject in _storage.Open(SelectedItems.OfType<IGameObjectMetadata>()))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Duplicate()
        {
            foreach (IGameObject gameObject in _factory.Duplicate(SelectedItems.OfType<IGameObjectMetadata>()))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Delete()
        {
            IEnumerable<IGameObjectMetadata> references = SelectedItems.OfType<IGameObjectMetadata>();
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

            IDialogParameters parameters = new DialogParameters
            {
                { Dialog.Title, "L5R NPC Creation Tool" },
                { Dialog.Confirmation.Content, confirmationContent }
            };

            _dialogService.ShowDialog(Dialog.Confirmation.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    _eventAggregator.GetEvent<ForceCloseGameObjects>().Publish(references);
                    _storage.Delete(references);
                }
            });
        }

        private void Export()
        {
            _eventAggregator.GetEvent<ExportGameObjectsEvent>().Publish(SelectedItems.OfType<IGameObjectReference>());
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsReferenceSelected));
        }
    }
}
