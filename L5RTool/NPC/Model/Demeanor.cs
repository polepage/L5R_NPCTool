using System.Collections.Generic;

namespace NPC.Model
{
    public class Demeanor: IElement
    {
        internal Demeanor()
        {
            Modifiers = new Dictionary<Ring, int>(5);
        }

        public string Name { get; set; }
        public string Unmasking { get; set; }
        public string Description { get; set; }

        public IDictionary<Ring, int> Modifiers { get; }

        public ElementType Type => ElementType.Demeanor;
    }
}
