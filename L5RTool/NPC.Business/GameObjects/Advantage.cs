﻿namespace NPC.Business.GameObjects
{
    class Advantage : Trait<Data.GameObjects.IAdvantage>, IAdvantage
    {
        public Advantage(Data.GameObjects.IAdvantage trait)
            : base(trait)
        {
        }
    }
}
