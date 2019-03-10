using Prism.Events;
using Prism.Ioc;
using Prism.Unity.Ioc;
using System.Windows;

namespace NPC.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainerExtension _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterContainer();
            RegisterComponents();
            InitializeDependencies();
        }

        private void RegisterContainer()
        {
            _container = new UnityContainerExtension();
            _container.RegisterInstance(_container);
        }

        private void RegisterComponents()
        {
            _container.RegisterSingleton<IEventAggregator, EventAggregator>();
        }

        private void InitializeDependencies()
        {
            Data.InitializationService.Initialize(_container);
            Business.InitializationService.Initialize(_container);
            Presenter.Windows.InitializationService.Initialize(_container, _container);
            Parser.InitializationService.Initialize(_container);
        }
    }
}
