namespace NPC.Presenter.GameObjects
{
    class Ability : GameObjectData<Data.GameObjects.IAbility>, IAbility, ICopyTarget
    {
        public Ability(Data.GameObjects.IAbility ability)
            : base(ability)
        {
        }

        public string Content
        {
            get => DataObject.Content;
            set => DataObject.Content = value;
        }

        public void CopyData(IGameObjectData copySource)
        {
            if (copySource is IAbility ability)
            {
                Content = ability.Content;
            }
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Content), nameof(Content));
        }
    }
}
