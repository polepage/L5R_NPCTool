using System;

namespace NPC.Presenter.GameObjects
{
    class InternalFactory
    {
        public IGameObject Create(Business.GameObjects.IGameObject source)
        {
            switch (source)
            {
                case Business.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Business.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                default:
                    throw new ArgumentOutOfRangeException("NPC.Presenter: Unknown type.");
                case null:
                    throw new ArgumentNullException("NPC.Presenter: Data object is null.");
            }
        }
    }
}
