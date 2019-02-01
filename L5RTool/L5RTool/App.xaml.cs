using L5RUI.ViewModels;
using L5RUI.Views;
using Prism.Mvvm;
using System.Windows;

namespace L5RTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            RegisterViews();
        }

        private void RegisterViews()
        {
            // Should remove those that are not injected anything (they can work via convention).
            ViewModelLocationProvider.Register<MainWindow>(() => new MainWindowViewModel());
            ViewModelLocationProvider.Register<MainMenu>(() => new MainMenuViewModel());
            ViewModelLocationProvider.Register<Database>(() => new DatabaseViewModel());
            ViewModelLocationProvider.Register<NewDialog>(() => new NewDialogViewModel());
        }
    }
}
