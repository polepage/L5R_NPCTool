using NPC.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPC.Data.GameObjects
{
    public interface ICharacter: IGameObject
    {
        CharacterType CharacterType { get; set; }
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


    }
}
