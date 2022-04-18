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
    internal class CustomTypeParser : InvocationParserBase
    {
        public CustomTypeParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.CustomType);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            IArgumentOperation stringOp = operation.Arguments[0];
            string name = CompilerHelper.GetValueFromOperation(stringOp.Value);
            if (name == null)
                throw CompilerHelper.Error(stringOp.Syntax, CompilationError.NotConstantValue);
            string @return = "@" + name.Trim('"');

            if (canBeInline || returnToVar == null)
                return @return;
            output.AppendCommand($"set {returnToVar} {@return}");
            return returnToVar;
        }
    }
}
