namespace NPC.Presenter.GameObjects
{
    class Demeanor: GameObject, IDemeanor
    {
        Data.GameObjects.IDemeanor _source;

        public Demeanor(Data.GameObjects.IDemeanor source)
            : base(source)
        {
            _source = source;
        }

        public Demeanor(Data.GameObjects.IDemeanor source, IDemeanor copySource)
            : base (source, copySource)
        {
            _source = source;
            Air = copySource.Air;
            Earth = copySource.Earth;
            Fire = copySource.Fire;
            Water = copySource.Water;
            Void = copySource.Void;
            Unmasking = copySource.Unmasking;
            Description = copySource.Description;
        }

        public int Air
        {
            get => _source.Air;
            set => _source.Air = value;
        }

        public int Earth
        {
            get => _source.Earth;
            set => _source.Earth = value;
        }

        public int Fire
        {
            get => _source.Fire;
            set => _source.Fire = value;
        }

        public int Water
        {
            get => _source.Water;
            set => _source.Water = value;
        }

        public int Void
        {
            get => _source.Void;
            set => _source.Void = value;
        }

        public string Unmasking
        {
            get => _source.Unmasking;
            set => _source.Unmasking = value;
        }

        public string Description
        {
            get => _source.Description;
            set => _source.Description = value;
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Air), nameof(Air));
            AddBinding(nameof(_source.Earth), nameof(Earth));
            AddBinding(nameof(_source.Fire), nameof(Fire));
            AddBinding(nameof(_source.Water), nameof(Water));
            AddBinding(nameof(_source.Void), nameof(Void));
            AddBinding(nameof(_source.Unmasking), nameof(Unmasking));
            AddBinding(nameof(_source.Description), nameof(Description));
        }
    }
}
