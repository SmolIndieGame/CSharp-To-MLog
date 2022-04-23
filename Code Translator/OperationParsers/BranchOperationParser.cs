using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    public class BranchOperationParser : OperationParserBase<IBranchOperation>
    {
        public BranchOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IBranchOperation operation, bool canBeInline, in string returnToVar)
        {
            switch (operation.BranchKind)
            {
                case BranchKind.Break:
                    output.AppendCommand($"jump {CompilerHelper.VarInCommand(TempValueType.LoopEnd, handler.currentLoopIndent.ToString())} always");
                    return null;
                case BranchKind.GoTo:
                    string to = handler.labelPos.TryGetValue(operation.Target.Name, out int pos)
                        ? pos.ToString()
                        : CompilerHelper.VarInCommand(TempValueType.Label, operation.Target.Name);
                    output.AppendCommand($"jump {to} always");
                    return null;
                case BranchKind.Continue:
                    output.AppendCommand($"jump {CompilerHelper.VarInCommand(TempValueType.LoopContinue, handler.currentLoopIndent.ToString())} always");
                    return null;
                case BranchKind.None:
                default:
                    throw CompilerHelper.Error(operation.Syntax, CompilationError.Unknown);
            }
        }
    }
}
