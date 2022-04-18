using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Translator.InvocationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Code_Translator.OperationParsers
{
    internal class InvocationOperationParser : OperationParserBase<IInvocationOperation>
    {
        readonly Dictionary<string, IInvocationParser> invocations;
        public readonly Dictionary<IParameterSymbol, string> funcArgs;

        public InvocationOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output)
        {
            invocations = new Dictionary<string, IInvocationParser>();
            Type[] alltypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var item in alltypes.Where(t => typeof(IInvocationParser).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var instance = Activator.CreateInstance(item, handler, this, output) as IInvocationParser;
                invocations.Add(instance.methodFullName, instance);
            }
            funcArgs = new Dictionary<IParameterSymbol, string>();
        }

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            IMethodSymbol method = operation.TargetMethod;
            if (!method.ContainingNamespace.IsGlobalNamespace || method.ContainingType.Name != handler.className)
                return HandleInvocation(operation, canBeInline, returnToVar);

            handler.OnMethodCalled(method);

            string jumpTo;
            if (handler.methodStartPos.TryGetValue(method, out int pos))
                jumpTo = pos.ToString();
            else
            {
                if (!handler.methodIndices.ContainsKey(method))
                    handler.methodIndices.Add(method, handler.methodIndices.Count);
                jumpTo = CompilerHelper.VarInCommand(TempValueType.Function, handler.methodIndices[method].ToString());
            }

            foreach (var arg in operation.Arguments)
            {
                if (!handler.funcArgIndices.ContainsKey(arg.Parameter))
                    handler.funcArgIndices.Add(arg.Parameter, handler.funcArgIndices.Count);
                string argName = $"arg{handler.funcArgIndices[arg.Parameter]}";
                handler.Handle(arg.Value, false, argName);
            }

            string tmpVar = output.GetNewTempVar();
            output.AppendCommand($"set {tmpVar} ptr");
            output.AppendCommand($"set ptr {output.nextLineIndex + 2}");
            output.AppendCommand($"jump {jumpTo} always");
            output.AppendCommand($"set ptr {tmpVar}");

            if (returnToVar != null && !method.ReturnsVoid)
            {
                output.AppendCommand($"set {returnToVar} ret");
                return returnToVar;
            }
            return null;
        }

        string HandleInvocation(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            var fullName = operation.TargetMethod.ToDisplayString(CompilerHelper.FullNameFormat);
            if (invocations.TryGetValue(fullName, out IInvocationParser parser))
                return parser.Parse(operation, canBeInline, returnToVar);
            if (operation.Instance == null && operation.TargetMethod.ContainingType != null)
            {
                string name = operation.TargetMethod.Name;
                string typeName = operation.TargetMethod.ContainingType.ToDisplayString(CompilerHelper.FullNameFormat);
                if (typeName == typeof(Mindustry).FullName)
                    return GenericCall(operation, name.ToLower(), returnToVar);
                if (typeName == typeof(UnitControl).FullName)
                    return GenericCall(operation, $"ucontrol {char.ToLower(name[0])}{name.AsSpan(1)}", returnToVar);
                if (typeName == typeof(Operation).FullName)
                    return GenericCall(operation, $"op {char.ToLower(name[0])}{name.AsSpan(1)}", returnToVar);
                if (typeName == typeof(Drawing).FullName)
                {
                    if (name.StartsWith("Draw"))
                        name = name.Substring(4);
                    if (name.StartsWith("Set"))
                        name = name.Substring(3);
                    return GenericCall(operation, $"draw {char.ToLower(name[0])}{name.AsSpan(1)}", returnToVar);
                }
            }

            throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedInvocation);
        }

        public string GenericCall(IInvocationOperation operation, string opName, in string returnTo)
        {
            StringBuilder builder = new(opName);
            if (!operation.TargetMethod.ReturnsVoid)
            {
                builder.Append(' ');
                builder.Append(returnTo ?? "_");
            }

            foreach (var op in operation.Arguments)
                funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            foreach (var param in operation.TargetMethod.Parameters)
                builder.Append(funcArgs[param]);

            output.AppendCommand(builder.ToString());
            return returnTo;
        }
    }
}
