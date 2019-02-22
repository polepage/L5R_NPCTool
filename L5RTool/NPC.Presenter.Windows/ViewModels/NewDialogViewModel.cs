using System.Windows.Input;
using NPC.Common;
using Prism.Commands;
using System.Collections.Generic;
using CS.Utils;
using Prism.Services.Dialogs;
using NPC.Presenter.Windows.Dialogs;

namespace NPC.Presenter.Windows.ViewModels
{
    class NewDialogViewModel : BaseDialogViewModel
    {
        private DelegateCommand _createCommand;
        public ICommand CreateCommand => _createCommand ?? (_createCommand = new DelegateCommand(Create));

        public IEnumerable<ObjectType> Types => EnumHelpers.GetValues<ObjectType>();

        private ObjectType _selection;
        public ObjectType Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);
            Selection = 0;
        }

        private void Create()
        {
            IDialogResult result = new DialogResult(true);
            result.Parameters.Add(Dialog.New.Type, Selection);

            RaiseRequestClose(result);
        }
    }
}
