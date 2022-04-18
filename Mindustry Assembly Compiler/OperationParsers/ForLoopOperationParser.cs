using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;

namespace Mindustry_Assembly_Compiler.OperationParsers
{
    internal class ForLoopOperationParser : OperationParserBase<IForLoopOperation>
    {

        public ForLoopOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IForLoopOperation operation, bool canBeInline, in string returnToVar)
        {
            foreach (var op in operation.Before)
                handler.Handle(op, false, null);
            int startPos = output.nextLineIndex;
            handler.HandleJump(operation.Condition, CompilerHelper.VarInCommand(TempValueType.LoopEnd, (++handler.currentLoopIndent).ToString()), false);
            handler.Handle(operation.Body, false, returnToVar);
            int continuePos = output.nextLineIndex;
            foreach (var op in operation.AtLoopBottom)
                handler.Handle(op, false, null);
            output.AppendCommand($"jump {startPos} always");
            output.SetValueToVarInCommand(TempValueType.LoopContinue, handler.currentLoopIndent.ToString(), continuePos.ToString());
            output.SetValueToVarInCommand(TempValueType.LoopEnd, handler.currentLoopIndent--.ToString(), output.nextLineIndex.ToString());
            return null;
        }
    }
}
