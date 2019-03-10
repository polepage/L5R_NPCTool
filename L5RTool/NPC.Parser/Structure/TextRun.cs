namespace NPC.Parser.Structure
{
    public class TextRun: InlineElement
    {
        internal TextRun(string text, bool isBold, bool isItalic)
        {
            Text = text;
            IsBold = isBold;
            IsItalic = isItalic;
        }

        public string Text { get; }
        public bool IsBold { get; }
        public bool IsItalic { get; }
    }
}
