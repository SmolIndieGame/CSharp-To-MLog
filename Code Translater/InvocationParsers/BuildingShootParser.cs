using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Text;

namespace Code_Translator.InvocationParsers
{
    internal class BuildingShootParser : InvocationParserBase
    {
        public BuildingShootParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Building);
        protected override string methodName => nameof(Building.Shoot);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return GenericInstanceCall(operation, "control shoot", returnToVar);
        }
    }
}
