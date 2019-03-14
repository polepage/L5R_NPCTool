using NPC.Common;
using NPC.Presenter.GameObjects;
using System;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Commands
{
    class ApplyGearTemplate : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is IGear;
        }

        public void Execute(object parameter)
        {
            if (parameter is IGear gear)
            {
                switch (gear.GearType)
                {
                    case GearType.Armor:
                        gear.Description = "Physical X, Supernatural Y";
                        break;
                    case GearType.Weapon:
                        gear.Description = "Range X, Damage Y, Deadliness Z";
                        break;
                    default:
                        gear.Description = "";
                        break;
                }
            }
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
