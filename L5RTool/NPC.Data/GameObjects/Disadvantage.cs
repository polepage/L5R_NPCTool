using NPC.Common;
using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Disadvantage : Trait, IDisadvantage
    {
        public Disadvantage()
            : base(ObjectType.Disadvantage)
        {
        }

        private Disadvantage(Guid id)
            : base(id, ObjectType.Disadvantage)
        {
        }

        public static GameObject FromXml(XElement xml)
        {
            return FromXml(xml, t =>
            {
                if (t.type != ObjectType.Disadvantage)
                {
                    throw new ArgumentException("Disadvantage.FromXml: xml is not a disadvantage.");
                }

                return t.id.HasValue ? new Disadvantage(t.id.Value) : new Disadvantage();
            });
        }
    }
}
