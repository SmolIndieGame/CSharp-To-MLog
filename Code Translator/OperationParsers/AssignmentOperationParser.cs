using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.OperationParsers
{
    public class AssignmentOperationParser : OperationParserBase<IAssignmentOperation>
    {
        public AssignmentOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output) { }

        public override string Parse(IAssignmentOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.Target.Type.IsType<LinkedBuilding>())
                throw CompilerHelper.Error(operation.Value.Syntax, CompilationError.SetLinkedBuilding);

            string name = handler.Handle(operation.Target, true, null);
            string @return = operation switch
            {
                ICompoundAssignmentOperation cao => handler.HandleBinary(cao.OperatorKind, cao.Target, cao.Value, true, name),
                ISimpleAssignmentOperation sao when !sao.IsRef => handler.Handle(sao.Value, true, name),
                _ => throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedOperation, operation)
            };

            if (@return == null)
                throw CompilerHelper.Error(operation.Value.Syntax, CompilationError.NoReturnValue);

            if (operation.Target is IPropertyReferenceOperation o)
            {
                GenericSetProperty(o, @return);

                if (canBeInline || returnToVar == null)
                    return @return;
                output.AppendCommand($"set {returnToVar} {@return}");
                return returnToVar;
            }

            if (@return != name)
                output.AppendCommand($"set {name} {@return}");

            if (canBeInline || returnToVar == null || returnToVar == name)
                return name;
            output.AppendCommand($"set {returnToVar} {name}");
            return returnToVar;
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
