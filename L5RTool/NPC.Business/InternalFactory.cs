using NPC.Business.GameObjects;
using NPC.Common;
using System;

namespace NPC.Business
{
    class InternalFactory
    {
        public IGameObject Create(Data.GameObjects.IGameObject source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("NPC.Business: Data object is null.");
            }

            switch (source.Type)
            {
                case ObjectType.Advantage:
                    return new Advantage(source as Data.GameObjects.IAdvantage);
                case ObjectType.Disadvantage:
                    return new Disadvantage(source as Data.GameObjects.IDisadvantage);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Business: Unknown type.");
            }
        }
    }
}
