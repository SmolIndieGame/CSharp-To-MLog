using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    internal class UnaryOperationParser : OperationParserBase<IUnaryOperation>
    {
        public UnaryOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IUnaryOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("op");
            builder.Append(' ');
            builder.Append(operation.OperatorKind switch
            {
                UnaryOperatorKind.BitwiseNegation or
                UnaryOperatorKind.Not => "not",
                UnaryOperatorKind.Plus => "add",
                UnaryOperatorKind.Minus => "sub",
                _ => throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedOperation, operation),
            });

            builder.Append(' ');
            builder.Append(returnToVar);
            if (operation.OperatorKind == UnaryOperatorKind.Plus || operation.OperatorKind == UnaryOperatorKind.Minus)
                builder.Append(" 0");
            builder.Append(handler.ToArgument(operation.Operand));
            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
