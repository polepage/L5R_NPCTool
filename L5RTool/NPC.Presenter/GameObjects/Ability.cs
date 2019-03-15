﻿namespace NPC.Presenter.GameObjects
{
    class Ability : GameObject, IAbility
    {
        private Data.GameObjects.IAbility _source;

        public Ability(Data.GameObjects.IAbility source)
            : base(source)
        {
            _source = source;
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
                Content = ability.Content;
            }
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Content), nameof(Content));
        }
    }
}