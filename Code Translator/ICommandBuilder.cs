﻿namespace Code_Translator
{
    public interface ICommandBuilder
    {
        int nextLineIndex { get; }

        void AppendCommand(string command);
        string GetNewTempVar();
        void SetValueToVarInCommand(TempValueType type, string index, string value);
    }
}