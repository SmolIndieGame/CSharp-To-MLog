using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.OperationParsers
{
    public class BinaryOperationParser : OperationParserBase<IBinaryOperation>
    {
        int indent;

        public BinaryOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override void Reset()
        {
            indent = 0;
        }

        public override string Parse(IBinaryOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.OperatorKind == BinaryOperatorKind.ConditionalAnd || operation.OperatorKind == BinaryOperatorKind.ConditionalOr)
            {
                if (!CompilerHelper.IsTempVar(returnToVar))
                    output.AppendCommand($"set {returnToVar} 0");
                handler.HandleJump(operation, CompilerHelper.VarInCommand(TempValueType.LogicAndOr, (++indent).ToString()), false);
                output.AppendCommand($"set {returnToVar} 1");
                output.SetValueToVarInCommand(TempValueType.LogicAndOr, indent--.ToString(), output.nextLineIndex.ToString());
                return returnToVar;
            }

            return handler.HandleBinary(operation.OperatorKind, operation.LeftOperand, operation.RightOperand, canBeInline, returnToVar);
        }
    }
}
