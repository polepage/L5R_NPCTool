using NPC.Common;

namespace NPC.Data.GameObjects
{
    public interface IAbility: IGameObject
    {
        AbilityType AbilityType { get; set; }
        string Content { get; set; }
    }
}
