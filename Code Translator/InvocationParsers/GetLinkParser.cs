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
    public class GetLinkParser : InvocationParserBase
    {
        public GetLinkParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.GetLink);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.TargetMethod.Parameters[0].Type.IsType<int>())
                return operationParser.GenericCall(operation, methodName.ToLower(), returnToVar);

            string name = null, idx = null;
            foreach (var op in operation.Arguments)
            {
                string value = CompilerHelper.GetValueFromOperation(op.Value);
                if (value == null)
                    throw CompilerHelper.Error(op.Syntax, CompilationError.NotConstantValue);
                if (op.Parameter.Type.IsType<BuildingType>())
                {
                    if (value == "null")
                        throw CompilerHelper.Error(op.Syntax, CompilationError.NoneEnumLiteral);
                    name = value.Substring(Math.Max(1, value.LastIndexOf('-') + 1));
                }
                else
                {
                    if (int.Parse(value) < 1)
                        throw CompilerHelper.Error(op.Syntax, CompilationError.InvalidLinkIndex);
                    idx = value;
                }
            }

            if (canBeInline || returnToVar == null)
                return name + idx;
            output.AppendCommand($"set {returnToVar} {name}{idx}");
            return returnToVar;
        }
    }
}
