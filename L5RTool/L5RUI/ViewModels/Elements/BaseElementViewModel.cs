using NPC;
using NPC.Model;
using Prism.Mvvm;
using System;
using System.Runtime.CompilerServices;

namespace L5RUI.ViewModels.Elements
{
    class BaseElementViewModel<T> : BindableBase, IElementViewModel where T: IElement
    {
        public BaseElementViewModel(T element)
        {
            TypedElement = element;
        }

        public ElementType Type => Element.Type;
        public IElement Element => TypedElement;

        protected T TypedElement { get; }

        protected bool SetProperty<TParam>(Action<TParam> setter, Func<TParam> getter, TParam value, [CallerMemberName] string propertyName = null)
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
