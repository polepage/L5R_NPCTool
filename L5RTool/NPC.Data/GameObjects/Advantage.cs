using NPC.Common;
using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Advantage : Trait, IAdvantage
    {
        public Advantage()
            : base(ObjectType.Advantage)
        {
        }

        private Advantage(Guid id)
            : base(id, ObjectType.Advantage)
        {
        }

        public static GameObject FromXml(XElement xml)
        {
            return FromXml(xml, t =>
            {
                if (t.type != ObjectType.Advantage)
                {
                    throw new ArgumentException("Advantage.FromXml: xml is not an advantage.");
                }

                return t.id.HasValue ? new Advantage(t.id.Value) : new Advantage();
            });
        }
    }
}
