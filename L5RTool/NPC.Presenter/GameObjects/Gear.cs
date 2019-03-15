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

        public Gear(Data.GameObjects.IGear source, IGear copySource)
            : base(source, copySource)
        {
            _source = source;
            Description = copySource.Description;
            GearType = copySource.GearType;
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

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Description), nameof(Description));
            AddBinding(nameof(_source.GearType), nameof(GearType));
        }
    }
}
