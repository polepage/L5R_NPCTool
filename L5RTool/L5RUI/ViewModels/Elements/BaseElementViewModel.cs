using NPC;
using NPC.Model;
using Prism.Mvvm;
using System;
using System.Runtime.CompilerServices;

namespace L5RUI.ViewModels.Elements
{
    abstract class BaseElementViewModel<T> : BindableBase, IElementViewModel where T: IElement
    {
        public BaseElementViewModel(T element)
        {
            TypedElement = element;
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
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
