using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;

namespace Code_Translator.OperationParsers
{
    public class WhileLoopOperationParser : OperationParserBase<IWhileLoopOperation>
    {

        public WhileLoopOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IWhileLoopOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.ConditionIsUntil)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedOperation, operation);

            int startPos = output.nextLineIndex;
            if (operation.ConditionIsTop)
            {
                handler.HandleJump(operation.Condition, CompilerHelper.VarInCommand(TempValueType.LoopEnd, (++handler.currentLoopIndent).ToString()), false);
                handler.Handle(operation.Body, false, null);
                output.AppendCommand($"jump {startPos} always");
            }
            else
            {
                handler.currentLoopIndent++;
                handler.Handle(operation.Body, false, null);
                handler.HandleJump(operation.Condition, startPos.ToString(), true);
            }
            output.SetValueToVarInCommand(TempValueType.LoopContinue, handler.currentLoopIndent.ToString(), startPos.ToString());
            output.SetValueToVarInCommand(TempValueType.LoopEnd, handler.currentLoopIndent--.ToString(), output.nextLineIndex.ToString());
            return null;
        }
    }
}
