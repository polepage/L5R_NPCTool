using System.Collections.Generic;
using CS.Utils.Collections;
using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    class Template: GameObject, ITemplate
    {
        private Data.GameObjects.ITemplate _source;

        public Template(Data.GameObjects.ITemplate source)
            : base(source)
        {
            _source = source;

            SuggestedAdvantages = new EnumerableWrapper<IAdvantage, Data.GameObjects.IAdvantage>(_source.SuggestedAdvantages, a => a.CreatePresenter() as IAdvantage);
            SuggestedDisadvantages = new EnumerableWrapper<IDisadvantage, Data.GameObjects.IDisadvantage>(_source.SuggestedDisadvantages, d => d.CreatePresenter() as IDisadvantage);
            SuggestedDemeanors = new EnumerableWrapper<IDemeanor, Data.GameObjects.IDemeanor>(_source.SuggestedDemeanors, d => d.CreatePresenter() as IDemeanor);
        }

        public int CombatConflictRank
        {
            get => _source.CombatConflictRank;
            set => _source.CombatConflictRank = value;
        }

        public int IntrigueConflictRank
        {
            get => _source.IntrigueConflictRank;
            set => _source.IntrigueConflictRank = value;
        }

        public int Air
        {
            get => _source.Air;
            set => _source.Air = value;
        }

        public int Earth
        {
            get => _source.Earth;
            set => _source.Earth = value;
        }

        public int Fire
        {
            get => _source.Fire;
            set => _source.Fire = value;
        }

        public int Water
        {
            get => _source.Water;
            set => _source.Water = value;
        }

        public int Void
        {
            get => _source.Void;
            set => _source.Void = value;
        }

        public int Artisan
        {
            get => _source.Artisan;
            set => _source.Artisan = value;
        }

        public int Martial
        {
            get => _source.Martial;
            set => _source.Martial = value;
        }

        public int Scholar
        {
            get => _source.Scholar;
            set => _source.Scholar = value;
        }

        public int Social
        {
            get => _source.Social;
            set => _source.Social = value;
        }

        public int Trade
        {
            get => _source.Trade;
            set => _source.Trade = value;
        }

        public int AdvantageRemplacements
        {
            get => _source.AdvantageRemplacements;
            set => _source.AdvantageRemplacements = value;
        }

        public int DisadvantageRemplacements
        {
            get => _source.DisadvantageRemplacements;
            set => _source.DisadvantageRemplacements = value;
        }

        public IEnumerable<IAdvantage> SuggestedAdvantages { get; }
        public IEnumerable<IDisadvantage> SuggestedDisadvantages { get; }
        public IEnumerable<IDemeanor> SuggestedDemeanors { get; }

        public int AbilityAdditions
        {
            get => _source.AbilityAdditions;
            set => _source.AbilityAdditions = value;
        }

        public ISet<AbilityType> AbilityTypes => _source.AbilityTypes;

        public void AddAdvantage()
        {
            _source.AddAdvantage();
        }

        public void AddDemeanor()
        {
            _source.AddDemeanor();
        }

        public void AddDisadvantage()
        {
            _source.AddDisadvantage();
        }

        public void RemoveAdvantage(IAdvantage advantage)
        {
            _source.RemoveAdvantage(advantage.GetSource() as Data.GameObjects.IAdvantage);
        }

        public void RemoveDemeanor(IDemeanor demeanor)
        {
            _source.RemoveDemeanor(demeanor.GetSource() as Data.GameObjects.IDemeanor);
        }

        public void RemoveDisadvantage(IDisadvantage disadvantage)
        {
            _source.RemoveDisadvantage(disadvantage.GetSource() as Data.GameObjects.IDisadvantage);
        }
    }
}
