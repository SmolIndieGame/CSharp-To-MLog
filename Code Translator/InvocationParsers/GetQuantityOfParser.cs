﻿using Microsoft.CodeAnalysis.Operations;
using Code_Translator.OperationParsers;
using MindustryLogics;
using System;
using System.Text;

namespace Code_Translator.InvocationParsers
{
    public class GetQuantityOfParser : InvocationParserBase
    {
        public GetQuantityOfParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Entity);
        protected override string methodName => nameof(Entity.GetQuantityOf);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return operationParser.GenericInstanceCall(operation, "sensor", returnToVar);
        }
    }
}
