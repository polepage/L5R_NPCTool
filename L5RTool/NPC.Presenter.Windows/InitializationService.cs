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
            container.Register<MainWindowViewModel>();
            container.Register<MainMenuViewModel>();
            container.Register<GameObjectEditorViewModel>();
            container.Register<GameObjectTreeViewModel>();
            container.Register<NewDialogViewModel>();
            container.Register<OpenDialogViewModel>();
            container.Register<SaveDialogViewModel>();

            ViewModelLocationProvider.Register<MainWindow>(() => provider.Resolve<MainWindowViewModel>());
            ViewModelLocationProvider.Register<MainMenu>(() => provider.Resolve<MainMenuViewModel>());
            ViewModelLocationProvider.Register<GameObjectEditor>(() => provider.Resolve<GameObjectEditorViewModel>());
            ViewModelLocationProvider.Register<GameObjectTree>(() => provider.Resolve<GameObjectTreeViewModel>());
            ViewModelLocationProvider.Register<NewDialog>(() => provider.Resolve<NewDialogViewModel>());
            ViewModelLocationProvider.Register<OpenDialog>(() => provider.Resolve<OpenDialogViewModel>());
            ViewModelLocationProvider.Register<SaveDialog>(() => provider.Resolve<SaveDialogViewModel>());
        }
    }
}
