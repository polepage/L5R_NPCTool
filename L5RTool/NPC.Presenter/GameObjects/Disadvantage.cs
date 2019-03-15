namespace NPC.Presenter.GameObjects
{
    class Disadvantage : Trait<Data.GameObjects.IDisadvantage>, IDisadvantage
    {
        public Disadvantage(Data.GameObjects.IDisadvantage trait)
            : base(trait)
        {
        }

        public Disadvantage(Data.GameObjects.IDisadvantage trait, IDisadvantage copySource)
            : base(trait, copySource)
        {
        }
    }
}
