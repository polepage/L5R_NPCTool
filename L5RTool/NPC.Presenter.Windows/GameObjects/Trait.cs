using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Collections;
using System.Collections.Generic;

namespace NPC.Presenter.Windows.GameObjects
{
    abstract class Trait<T>: BaseGameObject<T>, ITrait where T: Business.GameObjects.ITrait
    {
        private SetWrapper<SkillGroup> _skillGroups;
        private SetWrapper<TraitSphere> _spheres;

        public Trait(T trait)
            : base(trait)
        {
            _skillGroups = new SetWrapper<SkillGroup>(trait.SkillGroups);
            _spheres = new SetWrapper<TraitSphere>(trait.Spheres);
        }

        public string Name
        {
            get => GameObject.Name;
            set => GameObject.Name = value;
        }

        public string Description
        {
            get => GameObject.Description;
            set => GameObject.Description = value;
        }

        public Ring Ring
        {
            get => GameObject.Ring;
            set => GameObject.Ring = value;
        }

        public ISet<SkillGroup> SkillGroups => _skillGroups;
        public ISet<TraitSphere> Spheres => _spheres;

        public IEnumerable<Ring> RingList => EnumHelpers.GetValues<Ring>();
        public IEnumerable<SkillGroup> SkillGroupList => EnumHelpers.GetValues<SkillGroup>();
        public IEnumerable<TraitSphere> SpheresList => EnumHelpers.GetValues<TraitSphere>();

        protected override void RegisterBindings()
        {
            base.RegisterBindings();

            AddBinding(nameof(GameObject.Name), nameof(Name));
            AddBinding(nameof(GameObject.Description), nameof(Description));
            AddBinding(nameof(GameObject.Ring), nameof(Ring));
        }
    }
}
