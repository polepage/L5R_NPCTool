﻿using NPC.Common;
using System.Collections.Generic;

namespace NPC.Business.GameObjects
{
    public interface ITrait: IGameObjectData
    {
        string Description { get; set; }
        Ring Ring { get; set; }
        ISet<SkillGroup> SkillGroups { get; }
        ISet<TraitSphere> Spheres { get; }
    }
}