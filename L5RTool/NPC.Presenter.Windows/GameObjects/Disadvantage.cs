namespace NPC.Presenter.GameObjects
{
    class Disadvantage : Trait<Business.GameObjects.IDisadvantage>, IDisadvantage
    {
        public Disadvantage(Business.GameObjects.IDisadvantage trait)
            : base(trait)
        {
        }
    }
}
