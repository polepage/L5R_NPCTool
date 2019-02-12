using NPC.Common;
using NPC.Data.GameObjects;
using System;

namespace NPC.Data
{
    class InternalFactory
    {
        public IGameObject CreateEmpty(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Advantage:
                    return new Advantage();
                case ObjectType.Disadvantage:
                    return new Disadvantage();
                default:
                    throw new ArgumentOutOfRangeException("NPC.Data: Unknown object type.");
            }
        }
    }
}
