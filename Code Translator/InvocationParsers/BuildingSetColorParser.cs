using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class BuildingSetColorParser : InvocationParserBase
    {
        public BuildingSetColorParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Building);
        protected override string methodName => nameof(Building.SetColor);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return operationParser.GenericInstanceCall(operation, "control color", returnToVar);
        }
    }
}
