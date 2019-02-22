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

        public bool CanOpen => SelectedItems.OfType<ObjectReference>().Any();

        private IEnumerable<ObjectReferenceGroup> _references;
        public IEnumerable<ObjectReferenceGroup> References
        {
            get => _references;
            private set => SetProperty(ref _references, value);
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

            References = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectReferenceGroup(ot, parameters.GetValue<Business.IReferenceDatabase>(Dialog.Open.Source).References));
        }

        private void Open()
        {
            if (CanOpen)
            {
                IDialogResult result = new DialogResult(true);
                result.Parameters.Add(Dialog.Open.Selection, SelectedItems.OfType<ObjectReference>().ToList());

                RaiseRequestClose(result);
            }
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanOpen));
        }
    }
}
