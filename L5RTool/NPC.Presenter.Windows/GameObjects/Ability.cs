namespace NPC.Presenter.GameObjects
{
    class Ability : GameObjectData<Business.GameObjects.IAbility>, IAbility
    {
        public Ability(Business.GameObjects.IAbility ability)
            : base(ability)
        {
        }

        public string Content
        {
            get => DataObject.Content;
            set => DataObject.Content = value;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Content), nameof(Content));
        }
    }
}
