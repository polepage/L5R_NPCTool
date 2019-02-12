using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface ITrait
    {
        string Name { get; set; }
        string Description { get; set; }
        Ring Ring { get; set; }
        IList<SkillGroup> SkillGroups { get; }
        IList<TraitSphere> Spheres { get; }
    }
}
