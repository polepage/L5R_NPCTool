using NPC.Common;

namespace NPC.Business.GameObjects
{
    public interface IGear: IGameObjectData
    {
        GearType GearType { get; set; }
        string Description { get; set; }
    }
}
