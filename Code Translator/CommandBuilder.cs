using System.Text;

namespace Code_Transpiler
{
    public class CommandBuilder : ICommandBuilder
    {
        readonly StringBuilder sb;
        public int nextLineIndex { get; private set; }
        int currentTempVarIndex;

        public CommandBuilder(StringBuilder stringBuilder)
        {
            sb = stringBuilder;
            nextLineIndex = 0;
            currentTempVarIndex = -1;
        }

        public void AppendCommand(string command)
        {
            sb.AppendLine(command);
            nextLineIndex++;
        }

        public string GetNewTempVar()
        {
            return $"$$t{++currentTempVarIndex}";
        }

        public void SetValueToVarInCommand(TempValueType type, string index, string value)
        {
            sb.Replace(CompilerHelper.VarInCommand(type, index), value);
        }

        public void Clear()
        {
            sb.Clear();
            nextLineIndex = 0;
            currentTempVarIndex = -1;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
