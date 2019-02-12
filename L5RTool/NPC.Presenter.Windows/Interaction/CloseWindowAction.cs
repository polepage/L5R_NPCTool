using System.Windows;
using System.Windows.Interactivity;

namespace NPC.Presenter.Windows.Interaction
{
    class CloseWindowAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            AssociatedObject.Close();
        }
    }
}
