using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.OperationParsers
{
    public class DeclarationExpressionOperationParser : OperationParserBase<IDeclarationExpressionOperation>
    {
        public DeclarationExpressionOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IDeclarationExpressionOperation operation, bool canBeInline, in string returnToVar)
        {
            return handler.Handle(operation.Expression, canBeInline, returnToVar);
        }
    }
}
