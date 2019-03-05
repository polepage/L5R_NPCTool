namespace NPC.Presenter.GameObjects
{
    class Demeanor: GameObjectData<Business.GameObjects.IDemeanor>, IDemeanor
    {
        public Demeanor(Business.GameObjects.IDemeanor demeanor)
            : base(demeanor)
        {
        }

        public int Air
        {
            get => DataObject.Air;
            set => DataObject.Air = value;
        }

        public int Earth
        {
            get => DataObject.Earth;
            set => DataObject.Earth = value;
        }

        public int Fire
        {
            get => DataObject.Fire;
            set => DataObject.Fire = value;
        }

        public int Water
        {
            get => DataObject.Water;
            set => DataObject.Water = value;
        }

        public int Void
        {
            get => DataObject.Void;
            set => DataObject.Void = value;
        }

        public string Unmasking
        {
            get => DataObject.Unmasking;
            set => DataObject.Unmasking = value;
        }

        public string Description
        {
            get => DataObject.Description;
            set => DataObject.Description = value;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Air), nameof(Air));
            AddBinding(nameof(DataObject.Earth), nameof(Earth));
            AddBinding(nameof(DataObject.Fire), nameof(Fire));
            AddBinding(nameof(DataObject.Water), nameof(Water));
            AddBinding(nameof(DataObject.Void), nameof(Void));
            AddBinding(nameof(DataObject.Unmasking), nameof(Unmasking));
            AddBinding(nameof(DataObject.Description), nameof(Description));
        }
    }
}
