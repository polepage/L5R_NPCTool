using L5RUI.ViewModels;
using L5RUI.Views;
using NPC;
using NPC.Registration;
using Prism.Events;
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

            _container.RegisterSingleton<IEventAggregator, EventAggregator>();
            RegistrationService.Register(new UnityRegistrationDelegate(_container));
        }

        private void RegisterViews()
        {
            ViewModelLocationProvider.Register<MainMenu>(() =>
                        new MainMenuViewModel(_container.Resolve<IEventAggregator>(),
                                              _container.Resolve<IElementFactory>()));
        }
    }
}
