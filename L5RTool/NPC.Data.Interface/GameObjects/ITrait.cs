using NPC.Common;
using System.Collections.Generic;

namespace NPC.Data.GameObjects
{
    public interface ITrait: IGameObject 
    {
        string Name { get; set; }
        string Description { get; set; }
        Ring Ring { get; set; }
        ISet<SkillGroup> SkillGroups { get; }
        ISet<TraitSphere> Spheres { get; }
    }
}
