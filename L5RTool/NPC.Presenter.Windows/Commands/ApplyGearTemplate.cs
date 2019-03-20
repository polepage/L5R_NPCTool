using NPC.Common;
using NPC.Presenter.GameObjects;
using System;
using System.Windows;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Commands
{
    class ApplyGearTemplate : Freezable, ICommand
    {
        public static readonly DependencyProperty GearProperty =
            DependencyProperty.Register("Gear",
                                        typeof(IGear),
                                        typeof(ApplyGearTemplate));

        public IGear Gear
        {
            get => (IGear)GetValue(GearProperty);
            set => SetValue(GearProperty, value);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return Gear != null;
        }

        public void Execute(object parameter)
        {
            if (Gear != null)
            {
                switch (Gear.GearType)
                {
                    case GearType.Armor:
                        Gear.Description = "Physical X, Supernatural Y";
                        break;
                    case GearType.Weapon:
                        Gear.Description = "Range X, Damage Y, Deadliness Z";
                        break;
                    default:
                        Gear.Description = "";
                        break;
                }
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new ApplyGearTemplate();
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
