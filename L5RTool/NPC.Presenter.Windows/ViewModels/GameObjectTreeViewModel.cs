using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.Interaction;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
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
        private Business.IStorage _storage;
        private Business.IGameObjectFactory _factory;

        public GameObjectTreeViewModel(IEventAggregator eventAggregator, Business.IStorage storage, Business.IGameObjectFactory factory)
        {
            _eventAggregator = eventAggregator;
            _storage = storage;
            _factory = factory;

            References = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectReferenceGroup(ot, _storage.Database.References));

            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        public IEnumerable<ObjectReferenceGroup> References { get; }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public bool IsReferenceSelected => _selectedItems.OfType<ObjectReference>().Any();

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

        private DelegateCommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ?? (_duplicateCommand = new DelegateCommand(Duplicate));

        private DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        private void Open()
        {
            foreach (IGameObject gameObject in
                _storage.Open(
                        SelectedItems
                            .OfType<ObjectReference>()
                            .Select(or => or.Source))
                        .Select(go => new GameObject(go)))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Duplicate()
        {
            foreach (IGameObject gameObject in
                _factory.DuplicateReference(
                    SelectedItems
                        .OfType<ObjectReference>()
                        .Select(or => or.Source))
                    .Select(go => new GameObject(go)))
            {
                _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(gameObject);
            }
        }

        private void Delete()
        {
            //IEnumerable<ObjectReference> references = SelectedItems.OfType<ObjectReference>();
            //if (!references.Any())
            //{
            //    return;
            //}

            //string confirmationContent = " will be deleted permanently.";
            //if (references.Count() == 1)
            //{
            //    confirmationContent = "'" + references.First().Name + "'" + confirmationContent;
            //}
            //else
            //{
            //    confirmationContent = "The selected items" + confirmationContent;
            //}

            //var confirmation = new Confirmation
            //{
            //    Title = "L5R NPC Creation Tool",
            //    Content = confirmationContent
            //};

            //InteractionRequests.DeleteConfirmationRequest.Raise(confirmation);
            //if (confirmation.Confirmed)
            //{
            //    // Close objects if necessary
            //    // Delete references
            //}
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsReferenceSelected));
        }
    }
}
