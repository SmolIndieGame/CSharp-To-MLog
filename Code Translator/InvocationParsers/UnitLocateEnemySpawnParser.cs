using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Translator.InvocationParsers
{
    public class UnitLocateEnemySpawnParser : InvocationParserBase
    {
        public UnitLocateEnemySpawnParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.LocateEnemySpawn);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("ulocate spawn core 0 0");
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
