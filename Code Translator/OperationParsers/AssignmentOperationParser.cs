﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    internal class AssignmentOperationParser : OperationParserBase<IAssignmentOperation>
    {
        public AssignmentOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output) { }

        public override string Parse(IAssignmentOperation operation, bool canBeInline, in string returnToVar)
        {
            string name = handler.Handle(operation.Target, true, null);

            string @return = operation is ICompoundAssignmentOperation cao
                ? handler.HandleBinary(cao.OperatorKind, cao.Target, cao.Value, canBeInline, name)
                : handler.Handle(operation.Value, true, name);

            if (@return == null)
                throw CompilerHelper.Error(operation.Value.Syntax, CompilationError.NoReturnValue);
            
            if (operation.Target is IPropertyReferenceOperation o)
            {
                GenericSetProperty(o, @return);
                if (returnToVar != null && returnToVar != @return)
                    output.AppendCommand($"set {returnToVar} {@return}");
                return returnToVar;
            }

            if (@return != name)
                output.AppendCommand($"set {name} {@return}");
            if (returnToVar != null && returnToVar != name)
            {
                output.AppendCommand($"set {returnToVar} {name}");
                return returnToVar;
            }
            return name;
        }

        private void GenericSetProperty(IPropertyReferenceOperation operation, in string value)
        {
            string iName = handler.Handle(operation.Instance, true, output.GetNewTempVar());
            if (iName == null)
                throw CompilerHelper.Error(operation.Instance.Syntax, CompilationError.NoReturnValue);
            string pName = operation.Property.Name;
            output.AppendCommand($"control {char.ToLower(pName[0])}{pName.Substring(1)} {iName} {value}");
        }
    }
}