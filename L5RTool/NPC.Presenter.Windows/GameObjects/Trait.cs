using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NPC.Presenter.Windows.GameObjects
{
    abstract class Trait<T>: BaseGameObject<T>, ITrait where T: Business.GameObjects.ITrait
    {
        private ObservableCollection<SkillGroup> _skillGroups;
        private ObservableCollection<TraitSphere> _spheres;

        public Trait(T trait)
            : base(trait)
        {
            _skillGroups = new ObservableCollection<SkillGroup>();
            _spheres = new ObservableCollection<TraitSphere>();
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

        public IList<SkillGroup> SkillGroups => _skillGroups;
        public IList<TraitSphere> Spheres => _spheres;

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
