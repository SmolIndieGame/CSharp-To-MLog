using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.InvocationParsers
{
    public abstract class InvocationParserBase : IInvocationParser
    {
        protected readonly IOperationHandler handler;
        protected readonly IInvocationOperationParser operationParser;
        protected readonly ICommandBuilder output;

        protected abstract Type methodContainingType { get; }
        protected abstract string methodName { get; }
        public string methodFullName { get; }

        public InvocationParserBase(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output)
        {
            this.handler = handler;
            this.operationParser = operationParser;
            this.output = output;

            methodFullName = $"{methodContainingType.FullName}.{methodName}";
        }

        public virtual void Reset() { }

        public abstract string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar);
    }

    public interface IInvocationParser
    {
        string methodFullName { get; }

        string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar);
        void Reset();
    }
}
