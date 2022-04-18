﻿using Microsoft.CodeAnalysis.Operations;
using Mindustry_Assembly_Compiler.OperationParsers;
using MindustryLogics;
using System;
using System.Linq;
using System.Text;

namespace Mindustry_Assembly_Compiler.InvocationParsers
{
    internal class UnitLocateOreParser : InvocationParserBase
    {
        public UnitLocateOreParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.LocateOre);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            StringBuilder builder = new StringBuilder("ulocate ore core 0");
            foreach (var op in operation.Arguments)
                operationParser.funcArgs[op.Parameter] = handler.ToArgument(op.Value);
            foreach (var param in operation.TargetMethod.Parameters)
                builder.Append(operationParser.funcArgs[param]);

            builder.Append($" {returnToVar ?? "_"}");

            output.AppendCommand(builder.ToString());
            return returnToVar;
        }
    }
}
