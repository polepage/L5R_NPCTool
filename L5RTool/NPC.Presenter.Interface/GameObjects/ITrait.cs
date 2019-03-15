using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface ITrait: IGameObject
    {
        string Description { get; set; }
        Ring Ring { get; set; }
        ISet<SkillGroup> SkillGroups { get; }
        ISet<TraitSphere> Spheres { get; }
    }
}
