using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    public class BinaryOperationParser : OperationParserBase<IBinaryOperation>
    {
        public BinaryOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IBinaryOperation operation, bool canBeInline, in string returnToVar)
        {
            return handler.HandleBinary(operation.OperatorKind, operation.LeftOperand, operation.RightOperand, canBeInline, returnToVar);
        }
    }
}
