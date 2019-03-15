using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    class Gear : GameObject, IGear
    {
        Data.GameObjects.IGear _source;

        public Gear(Data.GameObjects.IGear source)
            : base(source)
        {
            _source = source;
        }
        
        public string Description
        {
            get => _source.Description;
            set => _source.Description = value;
        }

        public GearType GearType
        {
            get => _source.GearType;
            set => _source.GearType = value;
        }

        public override void CopyData(IGameObject copySource)
        {
            base.CopyData(copySource);
            if (copySource is IGear gear)
            {
                Description = gear.Description;
                GearType = gear.GearType;
            }
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Description), nameof(Description));
            AddBinding(nameof(_source.GearType), nameof(GearType));
        }
    }
}
