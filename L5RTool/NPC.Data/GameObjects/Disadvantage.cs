using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class Disadvantage : Trait, IDisadvantage
    {
        public Disadvantage()
            : base()
        {
            Type = ObjectType.Disadvantage;
        }
    }
}
