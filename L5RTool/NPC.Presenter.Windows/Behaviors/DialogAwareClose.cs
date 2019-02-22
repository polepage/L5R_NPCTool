using Microsoft.Xaml.Behaviors;
using Prism.Services.Dialogs;
using System.Windows;

namespace NPC.Presenter.Windows.Behaviors
{
    class DialogAwareClose: Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.DataContext is IDialogAware dialogAware)
            {
                dialogAware.RequestClose += OnRequestClose;
            }
        }

        private void OnRequestClose(IDialogResult obj)
        {
            AssociatedObject.Close();
        }
    }
}
