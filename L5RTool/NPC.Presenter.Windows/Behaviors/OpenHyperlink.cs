using Microsoft.Xaml.Behaviors;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace NPC.Presenter.Windows.Behaviors
{
    class OpenHyperlink: Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(NavigationRequested));
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(NavigationRequested));
            base.OnDetaching();
        }

        private void NavigationRequested(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
