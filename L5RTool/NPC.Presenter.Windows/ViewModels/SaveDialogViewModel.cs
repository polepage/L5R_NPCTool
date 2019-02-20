using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Interaction.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class SaveDialogViewModel : BindableBase, IInteractionRequestAware
    {
        private Action<IGameObject> _selector;

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));

        private DelegateCommand _dontSaveCommand;
        public ICommand DontSaveCommand => _dontSaveCommand ?? (_dontSaveCommand = new DelegateCommand(DontSave));

        private SaveConfirmation _confirmation;
        public INotification Notification
        {
            get => _confirmation;
            set
            {
                if (SetProperty(ref _confirmation, value as SaveConfirmation))
                {
                    DirtyObjects = _confirmation.Content as IEnumerable<IGameObject>;
                    _selector = _confirmation.Selector;
                }
            }
        }

        public Action FinishInteraction { get; set; }

        private IEnumerable<IGameObject> _dirtyObjects;
        public IEnumerable<IGameObject> DirtyObjects
        {
            get => _dirtyObjects;
            private set => SetProperty(ref _dirtyObjects, value);
        }

        private IGameObject _selectedItem;
        public IGameObject SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    _selector(_selectedItem);
                }
            }
        }

        private void Save()
        {
            _confirmation.Confirmed = true;
            _confirmation.Value = true;
            FinishInteraction?.Invoke();
        }

        private void DontSave()
        {
            _confirmation.Confirmed = true;
            FinishInteraction?.Invoke();
        }
    }
}
