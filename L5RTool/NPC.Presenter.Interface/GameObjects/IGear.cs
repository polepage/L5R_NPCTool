using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    public interface IGear: IGameObject
    {
        GearType GearType { get; set; }
        string Description { get; set; }
    }
}
