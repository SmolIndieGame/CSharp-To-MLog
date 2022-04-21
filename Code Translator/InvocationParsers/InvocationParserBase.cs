using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.InvocationParsers
{
    internal abstract class InvocationParserBase : IInvocationParser
    {
        protected readonly OperationHandler handler;
        protected readonly InvocationOperationParser operationParser;
        protected readonly CommandBuilder output;

        protected abstract Type methodContainingType { get; }
        protected abstract string methodName { get; }
        public string methodFullName { get; }

        public InvocationParserBase(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output)
        {
            this.handler = handler;
            this.operationParser = operationParser;
            this.output = output;

            methodFullName = $"{methodContainingType.FullName}.{methodName}";
        }

        /*protected static IArgumentOperation FindArgument<T>(IInvocationOperation operation)
        {
            IArgumentOperation argumentOperation = operation.Arguments.FirstOrDefault(op => CompilerHelper.IsType<T>(op.Parameter.Type));
            return argumentOperation;
        }

        protected static IArgumentOperation FindArgument(IInvocationOperation operation, string paramName)
        {
            IArgumentOperation argumentOperation = operation.Arguments.FirstOrDefault(op => op.Parameter.Name == paramName);
            return argumentOperation;
        }*/

        public virtual void Reset() { }

        protected string GenericInstanceCall(IInvocationOperation operation, string opName, in string returnTo)
        {
            string instance = handler.Handle(operation.Instance, true, output.GetNewTempVar());
            if (instance == null)
                throw CompilerHelper.Error(operation.Instance.Syntax, CompilationError.NoReturnValue);

            StringBuilder builder = new(opName);
            if (!operation.TargetMethod.ReturnsVoid)
            {
                builder.Append(' ');
                builder.Append(returnTo ?? "_");
            }
            builder.Append(' ');
            builder.Append(instance);
            foreach (var op in operation.Arguments)
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            foreach (var param in operation.TargetMethod.Parameters)
                builder.Append(operationParser.funcArgs[param]);
            output.AppendCommand(builder.ToString());
            return returnTo;
        }

        public abstract string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar);
    }

    public interface IInvocationParser
    {
        string methodFullName { get; }

        string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar);
        void Reset();
    }
}
