using NPC.Parser;
using Prism.Mvvm;

namespace NPC.Presenter.Windows.ViewModels
{
    class AbilityEditToolbarViewModel: BindableBase
    {
        public AbilityEditToolbarViewModel(IFormater formater)
        {
            Formater = formater;
        }

        public IFormater Formater { get; }
    }
}
