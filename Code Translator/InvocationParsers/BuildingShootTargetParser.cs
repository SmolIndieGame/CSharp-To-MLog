using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Text;

namespace Code_Translator.InvocationParsers
{
    public class BuildingShootTargetParser : InvocationParserBase
    {
        public BuildingShootTargetParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Building);
        protected override string methodName => nameof(Building.ShootTarget);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return operationParser.GenericInstanceCall(operation, "control shootp", returnToVar);
        }
    }
}
