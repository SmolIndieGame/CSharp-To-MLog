using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class UnitBindParser : InvocationParserBase
    {
        public UnitBindParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.UnitBind);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return operationParser.GenericCall(operation, "ubind", returnToVar);
        }
    }
}
