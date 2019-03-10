namespace NPC.Parser.Structure
{
    public class Symbol: InlineElement
    {
        internal Symbol(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
