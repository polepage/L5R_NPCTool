using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface ITemplate: IGameObject
    {
        int CombatConflictRank { get; set; }
        int IntrigueConflictRank { get; set; }

        int Air { get; set; }
        int Earth { get; set; }
        int Fire { get; set; }
        int Water { get; set; }
        int Void { get; set; }

        int Artisan { get; set; }
        int Martial { get; set; }
        int Scholar { get; set; }
        int Social { get; set; }
        int Trade { get; set; }

        int AdvantageRemplacements { get; set; }
        int DisadvantageRemplacements { get; set; }
        IEnumerable<IAdvantage> SuggestedAdvantages { get; }
        IEnumerable<IDisadvantage> SuggestedDisadvantages { get; }

        int AbilityAdditions { get; set; }
        ISet<AbilityType> AbilityTypes { get; }

        IEnumerable<IDemeanor> SuggestedDemeanors { get; }

        void AddAdvantage();
        void RemoveAdvantage(IAdvantage advantage);

        void AddDisadvantage();
        void RemoveDisadvantage(IDisadvantage disadvantage);

        void AddDemeanor();
        void RemoveDemeanor(IDemeanor demeanor);
    }
}
