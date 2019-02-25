using CS.Utils;
using CS.Utils.Collections;
using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    abstract class Trait<T>: GameObjectData<T>, ITrait where T: Business.GameObjects.ITrait
    {
        private SetWrapper<SkillGroup> _skillGroups;
        private SetWrapper<TraitSphere> _spheres;

        public Trait(T trait)
            : base(trait)
        {
            _skillGroups = new SetWrapper<SkillGroup>(trait.SkillGroups, (sk1, sk2) =>
            {
                return sk1.CompareTo(sk2);
            });
            _spheres = new SetWrapper<TraitSphere>(trait.Spheres, (ts1, ts2) =>
            {
                return ts1.CompareTo(ts2);
            });
        }

        public string Description
        {
            get => DataObject.Description;
            set => DataObject.Description = value;
        }

        public Ring Ring
        {
            get => DataObject.Ring;
            set => DataObject.Ring = value;
        }

        public ISet<SkillGroup> SkillGroups => _skillGroups;
        public ISet<TraitSphere> Spheres => _spheres;

        public IEnumerable<Ring> RingList => EnumHelpers.GetValues<Ring>();
        public IEnumerable<SkillGroup> SkillGroupList => EnumHelpers.GetValues<SkillGroup>();
        public IEnumerable<TraitSphere> SpheresList => EnumHelpers.GetValues<TraitSphere>();

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Description), nameof(Description));
            AddBinding(nameof(DataObject.Ring), nameof(Ring));
        }
    }
}
