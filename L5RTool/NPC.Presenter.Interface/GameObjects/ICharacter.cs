using NPC.Common;
using System.Collections.Generic;

namespace NPC.Presenter.GameObjects
{
    public interface ICharacter: IGameObject
    {
        CharacterType CharacterType { get; set; }
        bool HasSocietalValues { get; set; }
        int CombatConflictRank { get; set; }
        int IntrigueConflictRank { get; set; }
        string Description { get; set; }

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

        int Honor { get; set; }
        int Glory { get; set; }
        int Status { get; set; }

        int Endurance { get; set; }
        int Composure { get; set; }
        int Focus { get; set; }
        int Vigilance { get; set; }

        IDemeanor Demeanor { get; }

        IEnumerable<IAdvantage> Advantages { get; }
        IEnumerable<IDisadvantage> Disadvantages { get; }

        IEnumerable<IGear> FavoredWeapons { get; }
        IEnumerable<IGear> EquippedGear { get; }
        IEnumerable<IGear> OtherGear { get; }

        IEnumerable<IAbility> Abilities { get; }

        IAdvantage AddAdvantage();
        void RemoveAdvantage(IAdvantage advantage);

        IDisadvantage AddDisadvantage();
        void RemoveDisadvantage(IDisadvantage disadvantage);

        IGear AddFavoredWeapon();
        void RemoveFavoredWeapon(IGear gear);

        IGear AddEquipedGear();
        void RemoveEquipedGear(IGear gear);

        IGear AddOtherGear();
        void RemoveOtherGear(IGear gear);

        IAbility AddAbility();
        void RemoveAbility(IAbility ability);

        void ApplyTemplate(ITemplate template,
                           IEnumerable<IAdvantage> removedAdvantages, IEnumerable<IAdvantage> newAdvantages,
                           IEnumerable<IDisadvantage> removedDisadvantages, IEnumerable<IDisadvantage> newDisadvantages,
                           IEnumerable<IAbility> newAbilities, IDemeanor newDemeanor);
    }
}
