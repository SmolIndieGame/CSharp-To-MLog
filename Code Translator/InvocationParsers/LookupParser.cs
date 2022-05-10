using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class LookupParser : InvocationParserBase
    {
        public LookupParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.Lookup);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            string content = null, index = null;
            foreach (var op in operation.Arguments)
            {
                if (op.Parameter.Type.IsType<Content>())
                {
                    if (op.Value.ConstantValue.HasValue && op.Value.ConstantValue.Value == null)
                        throw CompilerHelper.Error(op.Syntax, CompilationError.NullLiteral);
                    if (op.Value is not IPropertyReferenceOperation pro)
                        throw CompilerHelper.Error(op.Value.Syntax, CompilationError.Unknown);
                    if (pro.Property.Name == "Building")
                    {
                        content = " block";
                        continue;
                    }    
                    content = $" {char.ToLower(pro.Property.Name[0])}{pro.Property.Name.Substring(1)}";
                    continue;
                }

                index = handler.ToArgument(op.Value);
            }

            output.AppendCommand($"lookup{content} {returnToVar ?? "_"}{index}");
            return returnToVar;
        }
    }
}
