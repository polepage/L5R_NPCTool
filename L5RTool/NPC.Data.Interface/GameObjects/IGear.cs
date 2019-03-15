using NPC.Common;

namespace NPC.Data.GameObjects
{
    public interface IGear: IGameObject
    {
        GearType GearType { get; set; }
        string Description { get; set; }
    }
}
