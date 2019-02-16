namespace NPC.Presenter.GameObjects
{
    class Advantage : Trait<Business.GameObjects.IAdvantage>, IAdvantage
    {
        public Advantage(Business.GameObjects.IAdvantage trait)
            : base(trait)
        {
        }
    }
}
