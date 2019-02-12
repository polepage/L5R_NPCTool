using NPC.Presenter.GameObjects;

namespace NPC.Presenter.Windows.GameObjects
{
    class Disadvantage : Trait<Business.GameObjects.IDisadvantage>, IDisadvantage
    {
        public Disadvantage(Business.GameObjects.IDisadvantage trait)
            : base(trait)
        {
        }
    }
}
