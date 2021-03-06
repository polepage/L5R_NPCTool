﻿namespace NPC.Parser
{
    public interface IFormater
    {
        string ApplyBold(string target);
        string ApplyItalic(string target);
        string InsertSymbol(string symbol);
        string AddIndent(string block);
        string RemoveIndent(string block);
    }
}
