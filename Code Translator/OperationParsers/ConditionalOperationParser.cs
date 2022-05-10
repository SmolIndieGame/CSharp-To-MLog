using Microsoft.CodeAnalysis.Operations;

namespace Code_Transpiler.OperationParsers
{
    public class ConditionalOperationParser : OperationParserBase<IConditionalOperation>
    {
        int indent;

        public ConditionalOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override void Reset()
        {
            indent = 0;
        }

        public override string Parse(IConditionalOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.WhenTrue == null && operation.WhenFalse == null) return returnToVar;

            handler.HandleJump(operation.Condition, CompilerHelper.VarInCommand(TempValueType.ConditionFalse, (++indent).ToString()), operation.WhenTrue == null);

            string @return;
            if (operation.WhenTrue == null)
            {
                @return = handler.Handle(operation.WhenFalse, false, returnToVar);
                output.SetValueToVarInCommand(TempValueType.ConditionFalse, (indent--).ToString(), output.nextLineIndex.ToString());
                return @return;
            }

            if (operation.WhenFalse == null)
            {
                @return = handler.Handle(operation.WhenTrue, false, returnToVar);
                output.SetValueToVarInCommand(TempValueType.ConditionFalse, (indent--).ToString(), output.nextLineIndex.ToString());
                return @return;
            }

            @return = handler.Handle(operation.WhenTrue, false, returnToVar);
            output.AppendCommand($"jump {CompilerHelper.VarInCommand(TempValueType.ConditionEnd, indent.ToString())} always");
            output.SetValueToVarInCommand(TempValueType.ConditionFalse, indent.ToString(), output.nextLineIndex.ToString());

            if (@return != handler.Handle(operation.WhenFalse, false, returnToVar) && returnToVar != null)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.Unknown);
            output.SetValueToVarInCommand(TempValueType.ConditionEnd, (indent--).ToString(), output.nextLineIndex.ToString());

            return @return;
        }
    }
}
