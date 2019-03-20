using CS.Utils;
using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.Windows.GameObjects
{
    static class GameObjectEnums
    {
        public static IEnumerable<Ring> Rings => EnumHelpers.GetValues<Ring>();
        public static IEnumerable<SkillGroup> SkillGroups => EnumHelpers.GetValues<SkillGroup>();
        public static IEnumerable<TraitSphere> Spheres => EnumHelpers.GetValues<TraitSphere>();
        public static IEnumerable<GearType> GearTypes => EnumHelpers.GetValues<GearType>();
        public static IEnumerable<CharacterType> CharacterTypes => EnumHelpers.GetValues<CharacterType>();
    }
}
