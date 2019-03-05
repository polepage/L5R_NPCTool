using CS.Utils;
using NPC.Common;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace NPC.Presenter.GameObjects
{
    class Gear : GameObjectData<Business.GameObjects.IGear>, IGear
    {
        public Gear(Business.GameObjects.IGear gear)
            : base(gear)
        {
        }

        public string Description
        {
            get => DataObject.Description;
            set => DataObject.Description = value;
        }

        public GearType GearType
        {
            get => DataObject.GearType;
            set => DataObject.GearType = value;
        }

        public IEnumerable<GearType> GearTypeList => EnumHelpers.GetValues<GearType>();

        private DelegateCommand _applyTemplateCommand;
        public ICommand ApplyTemplateCommand => _applyTemplateCommand ?? (_applyTemplateCommand = new DelegateCommand(ApplyTemplate));

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Description), nameof(Description));
            AddBinding(nameof(DataObject.GearType), nameof(GearType));
        }

        private void ApplyTemplate()
        {
            switch (GearType)
            {
                case GearType.Armor:
                    Description = "Physical X, Supernatural Y";
                    break;
                case GearType.Weapon:
                    Description = "Range X, Damage Y, Deadliness Z";
                    break;
                default:
                    Description = "";
                    break;
            }
        }
    }
}
