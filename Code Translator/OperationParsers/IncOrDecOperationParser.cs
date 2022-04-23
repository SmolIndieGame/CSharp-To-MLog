using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    public class IncOrDecOperationParser : OperationParserBase<IIncrementOrDecrementOperation>
    {
        public IncOrDecOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IIncrementOrDecrementOperation operation, bool canBeInline, in string returnToVar)
        {
            string name = handler.Handle(operation.Target, true, null);
            if (operation.IsPostfix && returnToVar != null)
                output.AppendCommand($"set {returnToVar} {name}");

            if (operation.Kind == OperationKind.Increment)
                output.AppendCommand($"op add {name} {name} 1");
            else
                output.AppendCommand($"op sub {name} {name} 1");

            if (!operation.IsPostfix && returnToVar != null && returnToVar != name)
            {
                output.AppendCommand($"set {returnToVar} {name}");
                return returnToVar;
            }
            return operation.IsPostfix ? returnToVar : name;
        }
    }
}
