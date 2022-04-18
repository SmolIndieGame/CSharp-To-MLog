using Microsoft.CodeAnalysis.Operations;

namespace Mindustry_Assembly_Compiler.OperationParsers
{
    internal class ConditionalOperationParser : OperationParserBase<IConditionalOperation>
    {
        public ConditionalOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IConditionalOperation operation, bool canBeInline, in string returnToVar)
        {
            return handler.HandleCondition(operation.Condition, operation.WhenTrue, operation.WhenFalse, returnToVar);
        }
    }
}
