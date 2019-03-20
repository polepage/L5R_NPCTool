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

        void AddAdvantage();
        void RemoveAdvantage(IAdvantage advantage);

        void AddDisadvantage();
        void RemoveDisadvantage(IDisadvantage disadvantage);

        void AddFavoredWeapon();
        void RemoveFavoredWeapon(IGear gear);

        void AddEquipedGear();
        void RemoveEquipedGear(IGear gear);

        void AddOtherGear();
        void RemoveOtherGear(IGear gear);

        void AddAbility();
        void RemoveAbility(IAbility ability);
    }
}
