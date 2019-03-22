using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class CharacterElementDialogViewModel: BaseDialogViewModel
    {
        private IStorage _storage;
        private Dictionary<IGameObjectReference, IGameObject> _cache;

        public CharacterElementDialogViewModel(IStorage storage, IParser parser)
        {
            _storage = storage;
            Parser = parser;
        }

        private DelegateCommand _acceptCommand;
        public ICommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new DelegateCommand(Accept));

        public IParser Parser { get; }

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

            _cache = new Dictionary<IGameObjectReference, IGameObject>();

            AvailableElements = parameters.GetValue<IEnumerable<IGameObjectMetadata>>(Dialog.CharacterElementSelection.Source);
        }

        private void OnSelectionChanged()
        {
            SelectedObject = SelectedItem != null ? Open(SelectedItem) : null;
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

        private IGameObject Open(IGameObjectMetadata reference)
        {
            if (_cache.TryGetValue(reference, out IGameObject gameObject))
            {
                return gameObject;
            }

            gameObject = _storage.Open(reference);
            _cache.Add(reference, gameObject);

            return gameObject;
        }
    }
}
