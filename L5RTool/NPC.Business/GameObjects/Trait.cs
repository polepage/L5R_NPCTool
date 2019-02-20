using System.Collections.Generic;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    abstract class Trait<T> : GameObjectData<T>, ITrait where T: Data.GameObjects.ITrait
    {
        public Trait(T trait)
            : base(trait)
        {
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

        public ISet<SkillGroup> SkillGroups => DataObject.SkillGroups;
        public ISet<TraitSphere> Spheres => DataObject.Spheres;

        public override void CopyData(IGameObjectData copySource)
        {
            if (copySource is ITrait trait)
            {
                Description = trait.Description;
                Ring = trait.Ring;
                SkillGroups.UnionWith(trait.SkillGroups);
                Spheres.UnionWith(trait.Spheres);
            }
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(DataObject.Description), nameof(Description));
            AddBinding(nameof(DataObject.Ring), nameof(Ring));
        }
    }
}
