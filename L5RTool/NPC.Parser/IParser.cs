using NPC.Parser.Structure;
using System.Collections.Generic;

namespace NPC.Parser
{
    public interface IParser
    {
        IEnumerable<BlockElement> Parse(string text);
    }
}
