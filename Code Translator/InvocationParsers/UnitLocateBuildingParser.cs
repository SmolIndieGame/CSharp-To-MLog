using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Transpiler.InvocationParsers
{
    public class UnitLocateBuildingParser : InvocationParserBase
    {
        public UnitLocateBuildingParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.LocateBuilding);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("ulocate building");
            foreach (var op in operation.Arguments)
            {
                if (op.Parameter.Type.IsType<BuildingGroup>())
                {
                    if (op.Value.ConstantValue.HasValue && op.Value.ConstantValue.Value == null)
                        throw CompilerHelper.Error(op.Syntax, CompilationError.NullLiteral);
                    if (op.Value is not IPropertyReferenceOperation pro)
                        throw CompilerHelper.Error(op.Value.Syntax, CompilationError.Unknown);
                    operationParser.funcArgs[op.Parameter] = $" {char.ToLower(pro.Property.Name[0])}{pro.Property.Name.Substring(1)}";
                    continue;
                }
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            }
            
            foreach (var param in operation.TargetMethod.Parameters)
            {
                builder.Append(operationParser.funcArgs[param]);
                if (param.Name == "isEnemy")
                    builder.Append(" 0");
                if (param.Name == "y")
                    builder.Append($" {returnToVar ?? "_"}");
            }

            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
