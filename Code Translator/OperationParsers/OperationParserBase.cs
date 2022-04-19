﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    public abstract class OperationParserBase<T> : IOperationParser where T : class, IOperation
    {
        protected readonly OperationHandler handler;
        protected readonly CommandBuilder output;
        
        public Type OperationType { get; }

        public OperationParserBase(OperationHandler handler, CommandBuilder output)
        {
            OperationType = typeof(T);
            this.handler = handler;
            this.output = output;
        }

        public abstract string Parse(T operation, bool canBeInline, in string returnToVar);

        string IOperationParser.Parse(IOperation operation, bool canBeInline, in string returnToVar)
        {
            return Parse(operation as T, canBeInline, returnToVar);
        }
    }

    public interface IOperationParser
    {
        Type OperationType { get; }

        string Parse(IOperation operation, bool canBeInline, in string returnToVar);
    }
}