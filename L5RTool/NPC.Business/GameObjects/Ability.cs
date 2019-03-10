namespace NPC.Business.GameObjects
{
    class Ability : GameObjectData<Data.GameObjects.IAbility>, IAbility
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

        public override void CopyData(IGameObjectData copySource)
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
