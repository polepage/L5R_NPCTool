using NPC.Presenter.Windows.Dialogs;
using Prism.Services.Dialogs;
using System.Reflection;

namespace NPC.Presenter.Windows.ViewModels
{
    class AboutDialogViewModel: BaseDialogViewModel
    {
        private string _version;
        public string Version
        {
            get => _version;
            private set => SetProperty(ref _version, value);
        }

        private string _copyright;
        public string Copyright
        {
            get => _copyright;
            private set => SetProperty(ref _copyright, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            Copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + ", v" + 
                      Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        }
    }
}
