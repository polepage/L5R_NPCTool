using NPC.Common;

namespace NPC.Data.GameObjects
{
    public interface IGear: IGameObjectData
    {
        GearType GearType { get; set; }
        string Description { get; set; }
    }
}
