namespace NPC.Business.GameObjects
{
    class Disadvantage : Trait<Data.GameObjects.IDisadvantage>, IDisadvantage
    {
        public Disadvantage(Data.GameObjects.IDisadvantage trait)
            : base(trait)
        {
        }
    }
}
