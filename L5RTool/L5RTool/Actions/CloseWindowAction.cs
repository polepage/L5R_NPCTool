using System.Windows;
using System.Windows.Interactivity;

namespace L5RTool.Actions
{
    class CloseWindowAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            AssociatedObject.Close();
        }
    }
}
