using System.Windows;
using System.Windows.Interactivity;

namespace L5RTool.Interaction
{
    class CloseWindowAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            AssociatedObject.Close();
        }
    }
}
