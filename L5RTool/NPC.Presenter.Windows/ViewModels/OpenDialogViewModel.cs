using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace NPC.Presenter.Windows.ViewModels
{
    class OpenDialogViewModel : BaseDialogViewModel
    {
        public OpenDialogViewModel()
        {
            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        private DelegateCommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new DelegateCommand(Open));

        public bool CanOpen => SelectedItems.OfType<GameObjectMetadata>().Any();

        private IEnumerable<ObjectMetadataGroup> _gameObjectGroups;
        public IEnumerable<ObjectMetadataGroup> GameObjectGroups
        {
            get => _gameObjectGroups;
            private set => SetProperty(ref _gameObjectGroups, value);
        }

        private IList _selectedItems;
        public IList SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            GameObjectGroups = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectMetadataGroup(ot, parameters.GetValue<Business.IManifest>(Dialog.Open.Source).GameObjects));
        }

        private void Open()
        {
            if (CanOpen)
            {
                IDialogResult result = new DialogResult(true);
                result.Parameters.Add(Dialog.Open.Selection, SelectedItems.OfType<GameObjectMetadata>().ToList());

                RaiseRequestClose(result);
            }
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanOpen));
        }
    }
}
