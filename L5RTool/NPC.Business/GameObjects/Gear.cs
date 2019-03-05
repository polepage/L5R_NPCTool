using NPC.Common;

namespace NPC.Business.GameObjects
{
    class Gear : GameObjectData<Data.GameObjects.IGear>, IGear
    {
        public Gear(Data.GameObjects.IGear gear)
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

        public override void CopyData(IGameObjectData copySource)
        {
            if (copySource is IGear gear)
            {
                Description = gear.Description;
                GearType = gear.GearType;
            }
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Description), nameof(Description));
            AddBinding(nameof(DataObject.GearType), nameof(GearType));
        }
    }
}
