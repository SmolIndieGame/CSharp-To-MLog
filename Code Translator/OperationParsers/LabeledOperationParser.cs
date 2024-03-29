﻿using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.OperationParsers
{
    public class LabeledOperationParser : OperationParserBase<ILabeledOperation>
    {
        public LabeledOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(ILabeledOperation operation, bool canBeInline, in string returnToVar)
        {
            handler.labelPos[operation.Label.Name] = output.nextLineIndex;
            output.SetValueToVarInCommand(TempValueType.Label, operation.Label.Name, output.nextLineIndex.ToString());
            return handler.Handle(operation.Operation, canBeInline, returnToVar);
        }
    }
}
