using System.Collections.Generic;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    abstract class Trait<T> : BaseGameObject<T>, ITrait where T: Data.GameObjects.ITrait
    {
        public Trait(T trait)
            : base(trait)
        {
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

        public ISet<SkillGroup> SkillGroups => GameObject.SkillGroups;
        public ISet<TraitSphere> Spheres => GameObject.Spheres;

        protected override void RegisterBindings()
        {
            base.RegisterBindings();

            AddBinding(nameof(GameObject.Name), nameof(Name));
            AddBinding(nameof(GameObject.Description), nameof(Description));
            AddBinding(nameof(GameObject.Ring), nameof(Ring));
        }
    }
}
