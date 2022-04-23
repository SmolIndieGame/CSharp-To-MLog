using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Code_Translator.InvocationParsers
{
    public class BuildingRadarParser : InvocationParserBase
    {
        public BuildingRadarParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Building);
        protected override string methodName => nameof(Building.Radar);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            string instance = handler.Handle(operation.Instance, true, output.GetNewTempVar());

            StringBuilder filter = new StringBuilder();
            string sort = null, order = null;
            foreach (var op in operation.Arguments)
            {
                if (op.Parameter.Type.IsType<RadarFilter>())
                {
                    var childOp = op.Value;
                    int i;
                    for (i = 0; i < 4 && childOp is IPropertyReferenceOperation pro; i++)
                    {
                        filter.Append(' ');
                        filter.Append(char.ToLower(pro.Property.Name[0]));
                        filter.Append(pro.Property.Name.AsSpan(1));
                        childOp = childOp.Children.FirstOrDefault();
                    }
                    if (i >= 4)
                        throw CompilerHelper.Error(op.Syntax, CompilationError.TooManyConstraints);
                    for (; i < 3; i++)
                        filter.Append(" any");
                }
                else if (op.Parameter.Type.IsType<SortMethod>())
                {
                    if (op.Value.ConstantValue.HasValue && op.Value.ConstantValue.Value == null)
                        throw CompilerHelper.Error(op.Syntax, CompilationError.NullLiteral);
                    if (op.Value is not IPropertyReferenceOperation pro)
                        throw CompilerHelper.Error(op.Value.Syntax, CompilationError.Unknown);
                    sort = $"{char.ToLower(pro.Property.Name[0])}{pro.Property.Name.Substring(1)}";
                }
                else
                {
                    order = handler.ToArgument(op.Value);
                }
            }

            output.AppendCommand($"radar{filter} {sort} {instance}{order} {returnToVar ?? "_"}");
            return returnToVar;
        }
    }
}
