using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    public interface IGear: IGameObjectData
    {
        GearType GearType { get; set; }
        string Description { get; set; }
    }
}
