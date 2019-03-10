using CS.Utils;
using NPC.Parser.Structure;
using System.Collections.Generic;

namespace NPC.Parser
{
    class Parser : IParser, IFormater
    {
        private readonly string _bold = "**";
        private readonly string _italic = "//";
        private readonly string _indent = "\n|";
        private readonly string _symbolStart = "{";
        private readonly string _symbolEnd = "}";
        private readonly string _newLine = "\n";

        public string ApplyBold(string target)
        {
            return _bold + target + _bold;
        }

        public string ApplyItalic(string target)
        {
            return _italic + target + _italic;
        }

        public string InsertSymbol(string symbol)
        {
            return _symbolStart + symbol + _symbolEnd;
        }

        public string IndentBlock(string block)
        {
            if (string.IsNullOrEmpty(block))
            {
                return _newLine + _indent;
            }

            string indentedBlock = block;
            if (block[0] != _newLine[0])
            {
                indentedBlock = _newLine + indentedBlock;
            }

            return indentedBlock.Replace(_newLine, _newLine + _indent);
        }

        public IEnumerable<BlockElement> Parse(string text)
        {
            var content = new List<BlockElement>();
            text?.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return content;
            }

            string currentString = text;
            BlockElement currentBlock = null;

            bool isBold = false;
            bool isItalic = false;
            int indentation = 0;

            while (!string.IsNullOrWhiteSpace(currentString))
            {
                bool isText = ExtractNextElement(ref currentString, out string element);
                string symbol = null;
                if (isText || ExtractSymbol(element, out symbol))
                {
                    InlineElement toAdd = null;
                    if (isText)
                    {
                        toAdd = new TextRun(element, isBold, isItalic);
                    }
                    else
                    {
                        toAdd = new Symbol(symbol);
                    }

                    if (currentBlock == null)
                    {
                        currentBlock = new BlockElement(indentation);
                        content.Add(currentBlock);
                    }
                    currentBlock.AddElement(toAdd);
                    continue;
                }

                if (element == _bold)
                {
                    isBold = !isBold;
                }
                else if (element == _italic)
                {
                    isItalic = !isItalic;
                }
                else
                {
                    currentBlock = null;
                    indentation++;
                }
            }

            return content;
        }

        private bool ExtractNextElement(ref string currentString, out string value)
        {
            value = ExtractText(ref currentString);
            if (string.IsNullOrEmpty(value))
            {
                value = ExtractSpecial(ref currentString);
                return false;
            }

            return true;
        }

        private string ExtractText(ref string currentString)
        {
            int nextSpecial = currentString.IndexOfAny(_bold, _italic, _indent, _symbolStart);
            string textValue;
            if (nextSpecial < 0)
            {
                textValue = currentString;
                currentString = string.Empty;
            }
            else
            {
                textValue = currentString.Substring(0, nextSpecial);
                currentString = currentString.Remove(0, nextSpecial);
            }

            return textValue;
        }

        private string ExtractSpecial(ref string currentString)
        {
            if (currentString.StartsWith(_bold))
            {
                currentString = currentString.Remove(0, _bold.Length);
                return _bold;
            }
            else if (currentString.StartsWith(_italic))
            {
                currentString = currentString.Remove(0, _italic.Length);
                return _italic;
            }
            else if (currentString.StartsWith(_indent))
            {
                currentString = currentString.Remove(0, _indent.Length);
                return _indent;
            }
            else if (currentString.StartsWith(_symbolStart))
            {
                int symbolEnd = currentString.IndexOf(_symbolEnd);
                string symbol = currentString.Substring(0, symbolEnd + 1);
                currentString = currentString.Remove(0, symbolEnd + 1);
                return symbol;
            }

            return string.Empty;
        }

        private bool ExtractSymbol(string value, out string symbol)
        {
            if (value.StartsWith(_symbolStart) && value.EndsWith(_symbolEnd))
            {
                symbol = value.Substring(1, value.Length - 2).Trim();
                return true;
            }

            symbol = null;
            return false;
        }
    }
}
