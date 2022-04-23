using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    public class ReturnOperationParser : OperationParserBase<IReturnOperation>
    {
        public ReturnOperationParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IReturnOperation operation, bool canBeInline, in string returnToVar)
        {
            if (operation.ReturnedValue != null)
                handler.Handle(operation.ReturnedValue, false, "ret");
            output.AppendCommand($"set @counter ptr");
            return null;
        }
    }
}
