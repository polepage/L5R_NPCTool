using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    public interface IAbility: IGameObject
    {
        AbilityType AbilityType { get; set; }
        string Content { get; set; }
    }
}
