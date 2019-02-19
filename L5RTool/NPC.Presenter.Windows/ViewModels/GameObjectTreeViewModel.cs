using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
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

        public GameObjectTreeViewModel(IEventAggregator eventAggregator, Business.IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _storage = storage;

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

        public bool CanOpen => _selectedItems.OfType<ObjectReference>().Any();

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

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

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanOpen));
        }
    }
}
