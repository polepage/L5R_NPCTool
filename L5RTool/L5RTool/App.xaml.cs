using L5RUI.ViewModels;
using L5RUI.Views;
using NPC.Registration;
using Prism.Mvvm;
using System.Windows;
using Unity;

namespace L5RTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IUnityContainer _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RegisterUnity();
            RegisterViews();
        }

        private void RegisterUnity()
        {
            _container = new UnityContainer();
            RegistrationService.Register(new UnityRegistrationDelegate(_container));
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
