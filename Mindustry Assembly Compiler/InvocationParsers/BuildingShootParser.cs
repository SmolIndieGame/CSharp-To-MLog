﻿using Microsoft.CodeAnalysis.Operations;
using Mindustry_Assembly_Compiler.OperationParsers;
using MindustryLogics;
using System;
using System.Text;

namespace Mindustry_Assembly_Compiler.InvocationParsers
{
    internal class BuildingShootParser : InvocationParserBase
    {
        public BuildingShootParser(OperationHandler handler, InvocationOperationParser operationParser, CommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Building);
        protected override string methodName => nameof(Building.Shoot);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            return GenericInstanceCall(operation, "control shoot", returnToVar);
        }
    }
}
