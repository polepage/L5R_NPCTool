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
    class SelectionDialogViewModel : BaseDialogViewModel
    {
        public SelectionDialogViewModel()
        {
            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        private DelegateCommand _acceptCommand;
        public ICommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new DelegateCommand(Accept));

        public ICommand SelectCommand => AcceptCommand;

        public bool CanAccept => SelectedItems.OfType<IGameObjectMetadata>().Any();

        private string _acceptText;
        public string AcceptText
        {
            get => _acceptText;
            set => SetProperty(ref _acceptText, value);
        }

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
            AcceptText = parameters.GetValue<string>(Dialog.Selection.Accept);

            GameObjectGroups = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectMetadataGroup(ot, parameters.GetValue<IManifest>(Dialog.Selection.Source).GameObjects));
        }

        private void Accept()
        {
            if (CanAccept)
            {
                IDialogResult result = new DialogResult(true);
                result.Parameters.Add(Dialog.Selection.SelectedItems, SelectedItems.OfType<IGameObjectMetadata>().ToList());

                RaiseRequestClose(result);
            }
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanAccept));
        }
    }
}
