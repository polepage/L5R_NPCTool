using NPC.Business.GameObjects;
using System;

namespace NPC.Business
{
    class InternalFactory
    {
        public IGameObject Create(Data.GameObjects.IGameObject source)
        {
            switch (source)
            {
                case Data.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Data.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Business: Unknown type.");
                case null:
                    throw new ArgumentNullException("NPC.Business: Data object is null.");
            }
        }
    }
}
