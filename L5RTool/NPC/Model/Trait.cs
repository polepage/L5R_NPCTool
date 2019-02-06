using System.Collections.Generic;

namespace NPC.Model
{
    public class Trait : IElement
    {
        public Trait()
        {
            SkillGroups = new HashSet<SkillGroup>();
            Spheres = new HashSet<TraitSphere>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Ring Ring { get; set; }
        public TraitType TraitType { get; set; }
        public ISet<SkillGroup> SkillGroups { get; }
        public ISet<TraitSphere> Spheres { get; }

        public ElementType Type => ElementType.Trait;
    }
}
