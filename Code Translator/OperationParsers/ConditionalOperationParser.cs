using Microsoft.CodeAnalysis.Operations;

namespace Code_Translator.OperationParsers
{
    public class ConditionalOperationParser : OperationParserBase<IConditionalOperation>
    {
        public ConditionalOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IConditionalOperation operation, bool canBeInline, in string returnToVar)
        {
            return handler.HandleCondition(operation.Condition, operation.WhenTrue, operation.WhenFalse, returnToVar);
        }
    }
}
