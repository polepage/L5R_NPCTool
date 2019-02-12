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
            InitializeDependencies();
            RegisterComponents(_container);
        }

        private void InitializeDependencies()
        {
            _container = new UnityContainerExtension();

            Data.InitializationService.Initialize(_container);
            Business.InitializationService.Initialize(_container);
            Presenter.Windows.InitializationService.Initialize(_container, _container);
        }

        private void RegisterComponents(IContainerRegistry container)
        {
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
        }
    }
}
