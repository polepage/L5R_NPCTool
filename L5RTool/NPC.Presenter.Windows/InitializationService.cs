using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.ViewModels;
using NPC.Presenter.Windows.Views;
using Prism.Ioc;
using Prism.Mvvm;

namespace NPC.Presenter.Windows
{
    public static class InitializationService
    {
        public static void Initialize(IContainerRegistry container, IContainerProvider provider)
        {
            container.Register<InternalFactory>();

            container.Register<MainWindowViewModel>();
            container.Register<MainMenuViewModel>();
            container.Register<GameObjectEditorViewModel>();
            container.Register<GameObjectTreeViewModel>();
            container.Register<NewDialogViewModel>();

            ViewModelLocationProvider.Register<MainWindow>(() => provider.Resolve<MainWindowViewModel>());
            ViewModelLocationProvider.Register<MainMenu>(() => provider.Resolve<MainMenuViewModel>());
            ViewModelLocationProvider.Register<GameObjectEditor>(() => provider.Resolve<GameObjectEditorViewModel>());
            ViewModelLocationProvider.Register<GameObjectTree>(() => provider.Resolve<GameObjectTreeViewModel>());
            ViewModelLocationProvider.Register<NewDialog>(() => provider.Resolve<NewDialogViewModel>());
        }
    }
}
