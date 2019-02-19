using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Interaction.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace NPC.Presenter.Windows.ViewModels
{
    class OpenDialogViewModel : BindableBase, IInteractionRequestAware
    {
        public OpenDialogViewModel(Business.IStorage storage)
        {
            References = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectReferenceGroup(ot, storage.Database.References));

            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

        private ValueConfirmation<IEnumerable<IObjectReference>> _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set => SetProperty(ref _confirmation, value as ValueConfirmation<IEnumerable<IObjectReference>>);
        }

        public Action FinishInteraction { get; set; }

        public bool CanOpen => SelectedItems.OfType<ObjectReference>().Any();

        public IEnumerable<ObjectReferenceGroup> References { get; }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private void Open()
        {
            if (CanOpen)
            {
                _confirmation.Confirmed = true;
                _confirmation.Value = SelectedItems.OfType<ObjectReference>().ToList();
                FinishInteraction?.Invoke();
            }
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanOpen));
        }
    }
}
