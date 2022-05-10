using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class UnitIsWithInParser : InvocationParserBase
    {
        public UnitIsWithInParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(UnitControl);
        protected override string methodName => nameof(UnitControl.IsWithin);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("ucontrol within");
            foreach (var op in operation.Arguments)
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            foreach (var param in operation.TargetMethod.Parameters)
                builder.Append(operationParser.funcArgs[param]);

            builder.Append($" {returnToVar ?? "_"}");

            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
