using NPC.Presenter.Windows.Binding;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Proxy;
using NPC.Presenter.Windows.Proxy.Data;
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
            container.RegisterDialog<SelectionDialog, SelectionDialogViewModel>(Dialog.Selection.Name);
            container.RegisterDialog<SaveDialog, SaveDialogViewModel>(Dialog.Save.Name);
            container.RegisterDialog<ConfirmationDialog, ConfirmationDialogViewModel>(Dialog.Confirmation.Name);
            container.RegisterDialog<PrintDialog, PrintDialogViewModel>(Dialog.Print.Name);
            container.RegisterDialog<AboutDialog, AboutDialogViewModel>(Dialog.About.Name);
            container.RegisterDialog<CharacterElementDialog, CharacterElementDialogViewModel>(Dialog.CharacterElementSelection.Name);
            container.RegisterDialog<ApplyTemplateDialog, ApplyTemplateDialogViewModel>(Dialog.ApplyTemplate.Name);

            ViewModelLocationProvider.Register<MainWindow>(() => provider.Resolve<MainWindowViewModel>());
            ViewModelLocationProvider.Register<MainMenu>(() => provider.Resolve<MainMenuViewModel>());
            ViewModelLocationProvider.Register<GameObjectEditor>(() => provider.Resolve<GameObjectEditorViewModel>());
            ViewModelLocationProvider.Register<GameObjectTree>(() => provider.Resolve<GameObjectTreeViewModel>());
            ViewModelLocationProvider.Register<AbilityEditToolbar>(() => provider.Resolve<AbilityEditToolbarViewModel>());

            ViewModelLocationProvider.Register<NewDialog>(() => provider.Resolve<NewDialogViewModel>());
            ViewModelLocationProvider.Register<SelectionDialog>(() => provider.Resolve<SelectionDialogViewModel>());
            ViewModelLocationProvider.Register<SaveDialog>(() => provider.Resolve<SaveDialogViewModel>());
            ViewModelLocationProvider.Register<ConfirmationDialog>(() => provider.Resolve<ConfirmationDialogViewModel>());
            ViewModelLocationProvider.Register<PrintDialog>(() => provider.Resolve<PrintDialogViewModel>());
            ViewModelLocationProvider.Register<AboutDialog>(() => provider.Resolve<AboutDialog>());
            ViewModelLocationProvider.Register<CharacterElementDialog>(() => provider.Resolve<CharacterElementDialogViewModel>());
            ViewModelLocationProvider.Register<ApplyTemplateDialog>(() => provider.Resolve<ApplyTemplateDialogViewModel>());

            ProxyDataLocator.Register<CharacterElementStorage>(() => provider.Resolve<CharacterElementStorageData>());
            ProxyDataLocator.Register<ApplyCharacterTemplate>(() => provider.Resolve<ApplyCharacterTemplateData>());
        }
    }
}
