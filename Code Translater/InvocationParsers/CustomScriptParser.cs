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
    internal class CustomScriptParser : InvocationParserBase
    {
        public CustomScriptParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.CustomScript);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            IOperation op = operation.Arguments[0].Value;
            if (!op.ConstantValue.HasValue)
                throw CompilerHelper.Error(op.Syntax, CompilationError.NotConstantValue);
            output.AppendCommand(op.ConstantValue.Value.ToString());
            return null;
        }
    }
}
