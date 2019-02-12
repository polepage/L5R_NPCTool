using NPC.Common;
using NPC.Presenter.GameObjects;
using System;

namespace NPC.Presenter.Windows.GameObjects
{
    class InternalFactory
    {
        public IGameObject Create(Business.GameObjects.IGameObject source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("NPC.Presenter: Data object is null.");
            }

            switch (source.Type)
            {
                case ObjectType.Advantage:
                    return new Advantage(source as Business.GameObjects.IAdvantage);
                case ObjectType.Disadvantage:
                    return new Disadvantage(source as Business.GameObjects.IDisadvantage);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Presenter: Unknown type.");
            }
        }
    }
}
