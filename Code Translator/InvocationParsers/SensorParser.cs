using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.InvocationParsers
{
    internal class SensorParser : InvocationParserBase
    {
        public SensorParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => null;

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            foreach (var op in operation.Arguments)
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);

            StringBuilder builder = new("sensor ");
            builder.Append(returnToVar ?? "_");
            builder.Append(operationParser.funcArgs[operation.TargetMethod.Parameters[0]]);
            builder.Append(operationParser.funcArgs[operation.TargetMethod.Parameters[1]]);
            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
