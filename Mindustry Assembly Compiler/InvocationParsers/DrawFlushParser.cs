using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Mindustry_Assembly_Compiler.OperationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindustry_Assembly_Compiler.InvocationParsers
{
    internal class DrawFlushParser : InvocationParserBase
    {
        public DrawFlushParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.DrawFlush);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.TargetMethod.Parameters[0].Type.IsType<Building>())
                return operationParser.GenericCall(operation, "drawflush", returnToVar);

            string value = CompilerHelper.GetValueFromOperation(operation.Arguments[0].Value);
            if (int.Parse(value) < 1)
                throw CompilerHelper.Error(operation.Arguments[0].Syntax, CompilationError.InvalidLinkIndex);
            output.AppendCommand($"drawflush display{value}");
            return null;
        }
    }
}
