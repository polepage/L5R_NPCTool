using NPC.Presenter.Windows.Dialogs;
using Prism.Services.Dialogs;

namespace NPC.Presenter.Windows.ViewModels
{
    class ApplyTemplateDialogViewModel: BaseDialogViewModel
    {
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);
        }
    }
}
