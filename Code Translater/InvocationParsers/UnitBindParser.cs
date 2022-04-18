using Microsoft.CodeAnalysis.Operations;
using Mindustry_Assembly_Compiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Mindustry_Assembly_Compiler.InvocationParsers
{
    internal class UnitBindParser : InvocationParserBase
    {
        public UnitBindParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
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
