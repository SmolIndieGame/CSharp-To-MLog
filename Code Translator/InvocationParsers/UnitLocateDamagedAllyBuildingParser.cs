using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class UnitLocateDamagedAllyBuildingParser : InvocationParserBase
    {
        public UnitLocateDamagedAllyBuildingParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.LocateDamagedAllyBuilding);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("ulocate damaged core 0 0");
            foreach (var op in operation.Arguments)
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            foreach (var param in operation.TargetMethod.Parameters)
            {
                builder.Append(operationParser.funcArgs[param]);
                if (param.Name == "y")
                    builder.Append($" {returnToVar ?? "_"}");
            }

            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
