using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    abstract class Trait<T>: GameObject, ITrait where T: Data.GameObjects.ITrait
    {
        private T _source;

        public Trait(T source)
            : base(source)
        {
            _source = source;
        }

        public Trait(T source, ITrait copySource)
            : base (source, copySource)
        {
            _source = source;
            Description = copySource.Description;
            Ring = copySource.Ring;
            SkillGroups.UnionWith(copySource.SkillGroups);
            Spheres.UnionWith(copySource.Spheres);
        }

        public string Description
        {
            get => _source.Description;
            set => _source.Description = value;
        }

        public Ring Ring
        {
            get => _source.Ring;
            set => _source.Ring = value;
        }

        public ISet<SkillGroup> SkillGroups => _source.SkillGroups;
        public ISet<TraitSphere> Spheres => _source.Spheres;

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.Description), nameof(Description));
            AddBinding(nameof(_source.Ring), nameof(Ring));
        }
    }
}
