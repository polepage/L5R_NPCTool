using Prism.Interactivity;
using System;
using System.ComponentModel;

namespace NPC.Presenter.Windows.Interaction
{
    class InvokeCancellableCommandAction: InvokeCommandAction
    {
        protected override void Invoke(object parameter)
        {
            if (parameter is CancelEventArgs args)
            {
                base.Invoke(new Action(() => args.Cancel = true));
            }
            else
            {
                base.Invoke(parameter);
            }
        }
    }
}
