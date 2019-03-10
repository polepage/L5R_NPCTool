using System.Collections.Generic;

namespace NPC.Parser.Structure
{
    public class BlockElement
    {
        private List<InlineElement> _elements;

        internal BlockElement(int indentation)
        {
            Indentation = indentation;
            _elements = new List<InlineElement>();
        }

        public int Indentation { get; }
        public IEnumerable<InlineElement> Elements => _elements;

        internal void AddElement(InlineElement element)
        {
            _elements.Add(element);
        }
    }
}
