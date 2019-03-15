namespace NPC.Presenter.GameObjects
{
    class Advantage : Trait<Data.GameObjects.IAdvantage>, IAdvantage
    {
        public Advantage(Data.GameObjects.IAdvantage trait)
            : base(trait)
        {
        }

        public Advantage(Data.GameObjects.IAdvantage trait, IAdvantage copySource)
            : base(trait, copySource)
        {
        }
    }
}
