using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class Advantage : Trait, IAdvantage
    {
        public Advantage()
            : base()
        {
            Type = ObjectType.Advantage;
        }
    }
}
