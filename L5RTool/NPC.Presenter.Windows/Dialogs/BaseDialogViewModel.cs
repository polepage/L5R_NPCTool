using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace NPC.Presenter.Windows.Dialogs
{
    class BaseDialogViewModel : BindableBase, IDialogAware
    {
        private string _iconSource;
        public string IconSource
        {
            get => _iconSource;
            set => SetProperty(ref _iconSource, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public event Action<IDialogResult> RequestClose;

        public virtual bool CanCloseDialog() { return true; }
        public virtual void OnDialogClosed() { }
        public virtual void OnDialogOpened(IDialogParameters parameters) { }

        protected virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}
