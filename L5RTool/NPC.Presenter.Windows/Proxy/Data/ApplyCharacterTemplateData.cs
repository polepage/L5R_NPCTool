using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Proxy.Data
{
    class ApplyCharacterTemplateData: BindableBase
    {
        private IDialogService _dialogService;

        public ApplyCharacterTemplateData(IFactory factory, IStorage storage, IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        private DelegateCommand<ICharacter> _applyTemplateCommand;
        public ICommand ApplyTemplateCommand => _applyTemplateCommand ?? (_applyTemplateCommand = new DelegateCommand<ICharacter>(ApplyTemplate));

        private void ApplyTemplate(ICharacter character)
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "Apply Character Template" },
                { Dialog.ApplyTemplate.Character, character }
            };

            _dialogService.ShowDialog(Dialog.ApplyTemplate.Name, parameters, dialogResult => { });
        }
    }
}
