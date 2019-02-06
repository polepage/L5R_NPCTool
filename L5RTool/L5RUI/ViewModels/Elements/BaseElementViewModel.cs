using NPC;
using NPC.Model;
using Prism.Mvvm;
using System;
using System.Runtime.CompilerServices;

namespace L5RUI.ViewModels.Elements
{
    class BaseElementViewModel : BindableBase, IElementViewModel
    {
        public BaseElementViewModel(IElement element)
        {
            Element = element;
        }

        public ElementType Type => Element.Type;
        public IElement Element { get; }

        protected bool SetProperty<T>(Action<T> setter, Func<T> getter, T value, [CallerMemberName] string propertyName = null)
        {
            if ((getter() != null && !getter().Equals(value)) || (getter() == null && value != null))
            {
                setter(value);
                RaisePropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}
