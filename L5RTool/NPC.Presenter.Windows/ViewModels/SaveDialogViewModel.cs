using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class SaveDialogViewModel : BaseDialogViewModel
    {
        private Action<IGameObject> _selector;

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));

        private DelegateCommand _dontSaveCommand;
        public ICommand DontSaveCommand => _dontSaveCommand ?? (_dontSaveCommand = new DelegateCommand(DontSave));

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

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            DirtyObjects = parameters.GetValue<IEnumerable<IGameObject>>(Dialog.Save.Items);
            _selector = parameters.GetValue<Action<IGameObject>>(Dialog.Save.Selector);
        }

        private void Save()
        {
            IDialogResult dialogResult = new DialogResult(true);
            dialogResult.Parameters.Add(Dialog.Save.NeedSave, true);
            RaiseRequestClose(dialogResult);
        }

        private void DontSave()
        {
            IDialogResult dialogResult = new DialogResult(true);
            dialogResult.Parameters.Add(Dialog.Save.NeedSave, false);
            RaiseRequestClose(dialogResult);
        }
    }
}
