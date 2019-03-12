using CS.Utils;
using NPC.Parser.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NPC.Parser
{
    class Parser : IParser, IFormater
    {
        private readonly string _bold = "**";
        private readonly string _italic = "//";
        private readonly string _indent = "|";
        private readonly string _symbolStart = "{";
        private readonly string _symbolEnd = "}";
        private readonly string _newLine = Environment.NewLine;

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

        public string AddIndent(string block)
        {
            if (string.IsNullOrEmpty(block))
            {
                return _indent;
            }

            string indentedBlock = block;
            if (!indentedBlock.StartsWith(_newLine))
            {
                indentedBlock = _indent + indentedBlock;
            }

            return indentedBlock.Replace(_newLine, _newLine + _indent);
        }

        public string RemoveIndent(string block)
        {
            if (string.IsNullOrEmpty(block))
            {
                return block;
            }

            string indentedBlock = block;
            if (indentedBlock.StartsWith(_indent))
            {
                indentedBlock = indentedBlock.Substring(_indent.Length);
            }

            return indentedBlock.Replace(_newLine + _indent, _newLine);
        }

        public IEnumerable<BlockElement> Parse(string text)
        {
            var content = new List<BlockElement>();
            text = text?.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                return content;
            }

            var textBlocks = SplitBlocks(text);
            
            bool isBold = false;
            bool isItalic = false;
            foreach ((string blockText, int blockIndentation) in textBlocks)
            {
                if (string.IsNullOrWhiteSpace(blockText))
                {
                    continue;
                }

                var block = new BlockElement(blockIndentation);
                content.Add(block);

                string currentString = blockText;
                while (!string.IsNullOrWhiteSpace(currentString))
                {
                    bool isText = ExtractNextElement(ref currentString, out string element);
                    if (isText)
                    {
                        block.AddElement(new TextRun(element, isBold, isItalic));
                    }
                    else if (element == _bold)
                    {
                        isBold = !isBold;
                    }
                    else if (element == _italic)
                    {
                        isItalic = !isItalic;
                    }
                    else if (ExtractSymbol(element, out string symbol))
                    {
                        block.AddElement(new Symbol(symbol));
                    }
                }
            }

            return content;
        }

        private IEnumerable<(string block, int indentation)> SplitBlocks(string text)
        {
            string[] lines = text.Split(Environment.NewLine);

            var indentedLines = new List<(string value, int indentation)>();
            foreach (string line in lines)
            {
                string lineText = line;
                int lineIndent = ExtractLineIndentation(ref lineText);
                indentedLines.Add((lineText, lineIndent));
            }

            var blocks = new List<(string value, int indentation)>();
            int blockStart = 0;
            int blockLength = 1;
            for (int i = 0; i < indentedLines.Count; i++)
            {
                if (i + 1 < indentedLines.Count && indentedLines[i].indentation == indentedLines[i + 1].indentation)
                {
                    blockLength++;
                    continue;
                }

                string blockText = string.Join(
                    Environment.NewLine,
                    indentedLines
                        .Skip(blockStart)
                        .Take(blockLength)
                        .Select(t => t.value));

                blocks.Add((blockText, indentedLines[i].indentation));

                blockStart = i + 1;
                blockLength = 1;
            }

            return blocks;
        }

        private int ExtractLineIndentation(ref string line)
        {
            int indentation = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line.StartsWith(_indent))
                {
                    line = line.Remove(0, _indent.Length);
                    indentation++;
                }
            }

            return indentation;
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
            int nextSpecial = currentString.IndexOfAny(_bold, _italic, _symbolStart);
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
