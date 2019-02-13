using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface ITrait
    {
        string Name { get; set; }
        string Description { get; set; }
        Ring Ring { get; set; }
        ISet<SkillGroup> SkillGroups { get; }
        ISet<TraitSphere> Spheres { get; }

        IEnumerable<Ring> RingList { get; }
        IEnumerable<SkillGroup> SkillGroupList { get; }
        IEnumerable<TraitSphere> SpheresList { get; }
    }
}
