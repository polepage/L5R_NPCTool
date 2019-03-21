using NPC.Common;

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

        public AbilityType AbilityType
        {
            get => _source.AbilityType;
            set => _source.AbilityType = value;
        }

        public string Content
        {
            get => _source.Content;
            set => _source.Content = value;
        }

        public override void CopyData(IGameObject copySource)
        {
            base.CopyData(copySource);
            if (copySource is IAbility ability)
            {
                AbilityType = ability.AbilityType;
                Content = ability.Content;
            }
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.AbilityType), nameof(AbilityType));
            AddBinding(nameof(_source.Content), nameof(Content));
        }
    }
}
