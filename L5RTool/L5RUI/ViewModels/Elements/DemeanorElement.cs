using NPC;
using NPC.Model;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace L5RUI.ViewModels.Elements
{
    class DemeanorElement : BindableBase, IElementViewModel
    {
        private Demeanor _element;
        private readonly ObservableCollection<DemeanorModifier> _modifiers;

        public DemeanorElement(Demeanor element)
        {
            _element = element;
            _modifiers = new ObservableCollection<DemeanorModifier>();
        }

        public ElementType Type => _element.Type;
        public IElement Element => _element;

        public string Name
        {
            get => _element.Name;
            set
            {
                if (value != _element.Name)
                {
                    _element.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _element.Description;
            set
            {
                if (value != _element.Description)
                {
                    _element.Description = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Unmasking
        {
            get => _element.Unmasking;
            set
            {
                if (value != _element.Unmasking)
                {
                    _element.Unmasking = value;
                    RaisePropertyChanged();
                }
            }
        }

        public IReadOnlyList<DemeanorModifier> Modifiers => _modifiers;
        
        private void InitModifiers()
        {
            foreach (Ring ring in _element.Modifiers.Keys)
            {
                var modifier = new DemeanorModifier(ring);
                modifier.Value = _element.Modifiers[ring];
                modifier.PropertyChanged += ModifierChanged;
                _modifiers.Add(modifier);
            }
        }

        private void AddModifier(Ring ring)
        {
            var modifier = new DemeanorModifier(ring);
            modifier.PropertyChanged += ModifierChanged;
            _modifiers.Add(modifier);

            _element.Modifiers.Add(ring, 0);
        }

        private void RemoveModifier(Ring ring)
        {
            
        }

        private void ModifierChanged(object sender, PropertyChangedEventArgs e)
        {
            var modifier = sender as DemeanorModifier;
            if (modifier != null)
            {
                _element.Modifiers[modifier.Ring] = modifier.Value;
            }
        }

        public class DemeanorModifier: BindableBase
        {
            public DemeanorModifier(Ring ring)
            {
                Ring = ring;
            }

            public Ring Ring { get; }

            private int _value;
            public int Value
            {
                get => _value;
                set => SetProperty(ref _value, value);
            }
        }
    }
}
