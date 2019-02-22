using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.ViewModels;
using NPC.Presenter.Windows.Views;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace NPC.Presenter.Windows
{
    public static class InitializationService
    {
        public static void Initialize(IContainerRegistry container, IContainerProvider provider)
        {
            container.Register<IDialogService, DialogService>();
            container.RegisterDialogWindow<DialogHost>();

            container.RegisterDialog<NewDialog, NewDialogViewModel>(Dialog.New.Name);
            container.RegisterDialog<OpenDialog, OpenDialogViewModel>(Dialog.Open.Name);
            container.RegisterDialog<SaveDialog, SaveDialogViewModel>(Dialog.Save.Name);
            container.RegisterDialog<ConfirmationDialog, ConfirmationDialogViewModel>(Dialog.Confirmation.Name);

            container.Register<ConfirmationDialog>(Dialog.Confirmation.Name);

            container.Register<MainWindowViewModel>();
            container.Register<MainMenuViewModel>();
            container.Register<GameObjectEditorViewModel>();
            container.Register<GameObjectTreeViewModel>();

            ViewModelLocationProvider.Register<MainWindow>(() => provider.Resolve<MainWindowViewModel>());
            ViewModelLocationProvider.Register<MainMenu>(() => provider.Resolve<MainMenuViewModel>());
            ViewModelLocationProvider.Register<GameObjectEditor>(() => provider.Resolve<GameObjectEditorViewModel>());
            ViewModelLocationProvider.Register<GameObjectTree>(() => provider.Resolve<GameObjectTreeViewModel>());
        }
    }
}
