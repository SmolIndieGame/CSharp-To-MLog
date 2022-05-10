using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.InvocationParsers
{
    public class PrintFlushParser : InvocationParserBase
    {
        public PrintFlushParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.PrintFlush);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.TargetMethod.Parameters[0].Type.IsType<Building>())
                return operationParser.GenericCall(operation, "printflush", returnToVar);

            string value = CompilerHelper.GetValueFromOperation(operation.Arguments[0].Value);
            if (int.Parse(value) < 1)
                throw CompilerHelper.Error(operation.Arguments[0].Syntax, CompilationError.InvalidLinkIndex);
            output.AppendCommand($"printflush message{value}");
            return null;
        }
    }
}
