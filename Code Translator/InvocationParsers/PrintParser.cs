﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.InvocationParsers
{
    public class PrintParser : InvocationParserBase
    {
        public PrintParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => nameof(Mindustry.Print);

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.Arguments.Length != 1 || operation.Arguments[0].Value is not IArrayCreationOperation aco)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.Unknown);

            foreach (var value in aco.Initializer.ElementValues)
                output.AppendCommand($"print{handler.ToArgument(value)}");
            return null;
        }
    }
}
