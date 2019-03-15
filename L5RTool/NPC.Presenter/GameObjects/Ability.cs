namespace NPC.Presenter.GameObjects
{
    class Ability : GameObject, IAbility
    {
        private Data.GameObjects.IAbility _source;

        public Ability(Data.GameObjects.IAbility source)
            : base(source)
        {
            _source = source;
        }

        public Ability(Data.GameObjects.IAbility source, IAbility copySource)
            : base(source, copySource)
        {
            _source = source;
            Content = copySource.Content;
        }

        public string Content
        {
            get => _source.Content;
            set => _source.Content = value;
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Content), nameof(Content));
        }
    }
}
