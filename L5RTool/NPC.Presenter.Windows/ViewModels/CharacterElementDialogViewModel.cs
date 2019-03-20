using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class CharacterElementDialogViewModel: BaseDialogViewModel
    {
        private Func<IGameObjectReference, IGameObject> _opener;

        private DelegateCommand _acceptCommand;
        public ICommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new DelegateCommand(Accept));

        private IParser _parser;
        public IParser Parser
        {
            get => _parser;
            private set => SetProperty(ref _parser, value);
        }

        private IGameObject _selectedObject;
        public IGameObject SelectedObject
        {
            get => _selectedObject;
            private set => SetProperty(ref _selectedObject, value);
        }

        private IGameObjectMetadata _selectedItem;
        public IGameObjectMetadata SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    OnSelectionChanged();
                }
            }
        }

        private IEnumerable<IGameObjectMetadata> _availableElements;
        public IEnumerable<IGameObjectMetadata> AvailableElements
        {
            get => _availableElements;
            private set => SetProperty(ref _availableElements, value);
        }

        public bool CanAccept => SelectedObject != null;

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            _opener = parameters.GetValue<Func<IGameObjectReference, IGameObject>>(Dialog.CharacterElementSelection.Opener);
            Parser = parameters.GetValue<IParser>(Dialog.CharacterElementSelection.Parser);

            AvailableElements = parameters.GetValue<IEnumerable<IGameObjectMetadata>>(Dialog.CharacterElementSelection.Source);
        }

        private void OnSelectionChanged()
        {
            SelectedObject = SelectedItem != null ? _opener(SelectedItem) : null;
            RaisePropertyChanged(nameof(CanAccept));
        }

        private void Accept()
        {
            if (CanAccept)
            {
                IDialogResult result = new DialogResult(true);
                result.Parameters.Add(Dialog.CharacterElementSelection.Selection, SelectedObject);

                RaiseRequestClose(result);
            }
        }
    }
}
