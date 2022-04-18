using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator
{
    class w: OperationWalker
    {
        public override void Visit(IOperation operation)
        {
            base.Visit(operation);
            Console.WriteLine($"{operation.Syntax}\t\t{operation.GetType().Name}");
        }
    }

    public sealed class OperationHandler
    {
        readonly SemanticModel semanticModel;
        public CommandBuilder output { get; }
        public Dictionary<IMethodSymbol, int> methodStartPos { get; }
        public Dictionary<IMethodSymbol, int> methodIndices { get; }
        public Dictionary<IParameterSymbol, int> funcArgIndices { get; }
        public string className { get; }

        readonly Dictionary<Type, IOperationParser> operations;
        readonly Action<IMethodSymbol> onMethodCalled;

        public int currentLoopIndent { get; set; }
        public Dictionary<string, int> labelPos { get; }
        public int currentConditionIndent { get; private set; }

        public OperationHandler(SemanticModel semanticModel, CommandBuilder output, Dictionary<IMethodSymbol, int> methodStartPos, Dictionary<IMethodSymbol, int> methodIndices, Dictionary<IParameterSymbol, int> funcArgIndices, string className, Action<IMethodSymbol> onMethodCalled)
        {
            this.semanticModel = semanticModel;
            this.output = output;

            operations = new Dictionary<Type, IOperationParser>();
            Type[] alltypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var item in alltypes.Where(t => typeof(IOperationParser).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var instance = Activator.CreateInstance(item, this, output) as IOperationParser;
                operations.Add(instance.OperationType, instance);
            }

            this.methodStartPos = methodStartPos;
            this.methodIndices = methodIndices;
            this.funcArgIndices = funcArgIndices;
            this.className = className;
            this.onMethodCalled = onMethodCalled;

            labelPos = new Dictionary<string, int>();
            currentLoopIndent = 0;
            currentConditionIndent = 0;
        }

        public string Handle(IOperation operation, bool canBeInline, in string returnToVar)
        {
            string @return;
            @return = CompilerHelper.GetValueFromOperation(operation);
            if (@return != null)
            {
                if (canBeInline || returnToVar == null)
                    return @return;
                output.AppendCommand($"set {returnToVar} {@return}");
                return returnToVar;
            }

            switch (operation)
            {
                case IConversionOperation o:
                    if (o.Operand.Type.IsTypeEnum() && o.Type.SpecialType != SpecialType.System_Object)
                        throw CompilerHelper.Error(o.Syntax, CompilationError.CastingEnum);
                    return Handle(o.Operand, canBeInline, returnToVar);
                case IExpressionStatementOperation o:
                    return Handle(o.Operation, false, null);
                case IVariableDeclarationGroupOperation o:
                    foreach (var op in o.Declarations)
                        Handle(op, false, null);
                    return null;
                case IBlockOperation o:
                    foreach (var op in o.Operations)
                        Handle(op, false, null);
                    return null;
                case IInstanceReferenceOperation o:
                    if (o.ReferenceKind != InstanceReferenceKind.ContainingTypeInstance)
                        throw CompilerHelper.Error(o.Syntax, CompilationError.Unknown);
                    return "@this";
                case IDiscardOperation or ILocalReferenceOperation or IFieldReferenceOperation or IParameterReferenceOperation:
                    @return = GetVariableName(operation);
                    if (canBeInline || returnToVar == null)
                        return @return;
                    output.AppendCommand($"set {returnToVar} {@return}");
                    return returnToVar;
            }

            var interfaces = operation.GetType().GetInterfaces();
            for (int i = interfaces.Length - 1; i >= 0; i--)
                if (operations.TryGetValue(interfaces[i], out IOperationParser parser))
                    return parser.Parse(operation, canBeInline, returnToVar);

            throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedOperation, operation);
        }

        public string HandleBinary(BinaryOperatorKind operatorKind, IOperation leftOperand, IOperation rightOperand, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("op");
            if (operatorKind == BinaryOperatorKind.ConditionalAnd)
            {
                if (!CompilerHelper.IsTempVar(returnToVar))
                    output.AppendCommand($"set {returnToVar} 0");
                return HandleCondition(leftOperand, rightOperand, null, returnToVar);
            }
            if (operatorKind == BinaryOperatorKind.ConditionalOr)
            {
                output.AppendCommand($"set {returnToVar} 1");
                return HandleCondition(leftOperand, null, rightOperand, returnToVar);
            }
            if (operatorKind == BinaryOperatorKind.NotEquals)
            {
                string lvalue = CompilerHelper.GetValueFromOperation(leftOperand);
                string rvalue = CompilerHelper.GetValueFromOperation(rightOperand);
                string @return = null;
                if (lvalue == "null")
                    @return = Handle(rightOperand, canBeInline, returnToVar);
                if (rvalue == "null")
                    @return = Handle(leftOperand, canBeInline, returnToVar);
                if (@return != null)
                {
                    if (canBeInline || returnToVar == null)
                        return @return;
                    output.AppendCommand($"set {returnToVar} {@return}");
                    return returnToVar;
                }
            }

            builder.Append(' ');
            builder.Append(GetOperatorString(operatorKind, leftOperand, rightOperand));
            builder.Append(' ');
            builder.Append(returnToVar);
            builder.Append(ToArgument(leftOperand));
            builder.Append(ToArgument(rightOperand));
            output.AppendCommand(builder.ToString());
            return returnToVar;
        }

        public string HandleCondition(IOperation condition, IOperation whenTrue, IOperation whenFalse, in string returnToVar)
        {
            if (whenTrue == null && whenFalse == null) return returnToVar;

            HandleJump(condition, CompilerHelper.VarInCommand(TempValueType.Condition, (++currentConditionIndent).ToString()), whenTrue == null);

            string @return;
            if (whenTrue == null)
            {
                @return = Handle(whenFalse, false, returnToVar);
                output.SetValueToVarInCommand(TempValueType.Condition, (currentConditionIndent--).ToString(), output.nextLineIndex.ToString());
                return @return;
            }

            if (whenFalse == null)
            {
                @return = Handle(whenTrue, false, returnToVar);
                output.SetValueToVarInCommand(TempValueType.Condition, (currentConditionIndent--).ToString(), output.nextLineIndex.ToString());
                return @return;
            }

            @return = Handle(whenTrue, false, returnToVar);
            output.AppendCommand($"jump {CompilerHelper.VarInCommand(TempValueType.Condition, "0")} always");
            output.SetValueToVarInCommand(TempValueType.Condition, (currentConditionIndent--).ToString(), output.nextLineIndex.ToString());

            if (@return != Handle(whenFalse, false, returnToVar) && returnToVar != null)
                throw CompilerHelper.Error(condition.Syntax, CompilationError.Unknown);
            output.SetValueToVarInCommand(TempValueType.Condition, "0", output.nextLineIndex.ToString());

            return @return;
        }

        public void HandleJump(IOperation condition, string jumpToLine, bool jumpIf)
        {
            if (condition is IUnaryOperation uo && uo.OperatorKind == UnaryOperatorKind.Not)
            {
                jumpIf = !jumpIf;
                condition = uo.Operand;
            }

            if (condition is IBinaryOperation o)
            {
                StringBuilder builder = new StringBuilder("jump ");
                builder.Append(jumpToLine);
                builder.Append(' ');
                string opString = jumpIf
                    ? o.OperatorKind switch
                    {
                        BinaryOperatorKind.Equals => "equal",
                        BinaryOperatorKind.NotEquals => "notEqual",
                        BinaryOperatorKind.LessThan => "lessThan",
                        BinaryOperatorKind.LessThanOrEqual => "lessThanEq",
                        BinaryOperatorKind.GreaterThanOrEqual => "greaterThanEq",
                        BinaryOperatorKind.GreaterThan => "greaterThan",
                        _ => null
                    }
                    : o.OperatorKind switch
                    {
                        BinaryOperatorKind.Equals => "notEqual",
                        BinaryOperatorKind.NotEquals => "equal",
                        BinaryOperatorKind.LessThan => "greaterThanEq",
                        BinaryOperatorKind.LessThanOrEqual => "greaterThan",
                        BinaryOperatorKind.GreaterThanOrEqual => "lessThan",
                        BinaryOperatorKind.GreaterThan => "lessThanEq",
                        _ => null
                    };

                if (opString != null)
                {
                    builder.Append(opString);
                    builder.Append(ToArgument(o.LeftOperand));
                    builder.Append(ToArgument(o.RightOperand));
                    output.AppendCommand(builder.ToString());
                    return;
                }
            }

            string @return = Handle(condition, true, output.GetNewTempVar());
            if (@return == null)
                throw CompilerHelper.Error(condition.Syntax, CompilationError.NoReturnValue);
            output.AppendCommand($"jump {jumpToLine} {(jumpIf ? "notEqual" : "equal")} {@return} 0");
        }

        string GetVariableName(IOperation referenceOperation)
        {
            string name;
            if (referenceOperation is ILocalReferenceOperation lro)
                name = lro.Local.Name;
            else if (referenceOperation is IFieldReferenceOperation fro && fro.Field.ContainingType.Name == className)
                name = $"${fro.Field.Name}";
            else if (referenceOperation is IDiscardOperation)
                name = "_";
            else if (referenceOperation is IParameterReferenceOperation pro)
            {
                if (!funcArgIndices.ContainsKey(pro.Parameter))
                    throw CompilerHelper.Error(pro.Syntax, CompilationError.Unknown);
                name = $"arg{funcArgIndices[pro.Parameter]}";
            }
            else
                throw CompilerHelper.Error(referenceOperation.Syntax, CompilationError.UnsupportedOperation, referenceOperation);
            return name;
        }

        private static string GetOperatorString(BinaryOperatorKind operatorKind, IOperation leftOperand, IOperation rightOperand)
        {
            if (operatorKind == BinaryOperatorKind.Add)
            {
                var lType = leftOperand.Type?.SpecialType;
                var rType = rightOperand.Type?.SpecialType;
                if (lType == SpecialType.System_String || rType == SpecialType.System_String)
                    throw CompilerHelper.Error(rightOperand.Parent.Syntax, CompilationError.StringAddition);
            }

            if (operatorKind == BinaryOperatorKind.Divide)
            {
                var lType = leftOperand.Type?.SpecialType;
                var rType = rightOperand.Type?.SpecialType;
                if (lType == SpecialType.System_Single
                    || lType == SpecialType.System_Double
                    || lType == SpecialType.System_Decimal
                    || rType == SpecialType.System_Single
                    || rType == SpecialType.System_Double
                    || rType == SpecialType.System_Decimal)
                    return "div";
                return "idiv";
            }

            return operatorKind switch
            {
                BinaryOperatorKind.Add => "add",
                BinaryOperatorKind.Subtract => "sub",
                BinaryOperatorKind.Multiply => "mul",
                BinaryOperatorKind.Divide => "div",
                BinaryOperatorKind.Remainder => "mod",
                BinaryOperatorKind.LeftShift => "shl",
                BinaryOperatorKind.RightShift => "shr",
                BinaryOperatorKind.And => "and",
                BinaryOperatorKind.Or => "or",
                BinaryOperatorKind.ExclusiveOr => "xor",
                BinaryOperatorKind.ConditionalAnd => "land",
                BinaryOperatorKind.ConditionalOr => "or",
                BinaryOperatorKind.Equals => "equal",
                BinaryOperatorKind.NotEquals => "notEqual",
                BinaryOperatorKind.LessThan => "lessThan",
                BinaryOperatorKind.LessThanOrEqual => "lessThanEq",
                BinaryOperatorKind.GreaterThanOrEqual => "greaterThanEq",
                BinaryOperatorKind.GreaterThan => "greaterThan",
                _ => throw CompilerHelper.Error(rightOperand.Parent.Syntax, CompilationError.UnsupportedOperation, rightOperand.Parent)
            };
        }

        public string ToArgument(IOperation operation)
        {
            string @return = Handle(operation, true, output.GetNewTempVar());
            if (@return == null)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.NoReturnValue);

            return string.Concat(" ", @return);
        }

        public void OnMethodCalled(IMethodSymbol callee)
        {
            onMethodCalled?.Invoke(callee);
        }
    }
}
