using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Translator.OperationParsers
{
    internal class VariableDeclarationOperationParser : OperationParserBase<IVariableDeclarationOperation>
    {
        public VariableDeclarationOperationParser(OperationHandler handler, CommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IVariableDeclarationOperation operation, bool canBeInline, in string returnToVar)
        {
            foreach (var declarator in operation.Declarators)
            {
                if (!CompilerHelper.IsTypeAllowed(declarator.Symbol.Type))
                    throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedType);
                var initializer = declarator.GetVariableInitializer();
                if (initializer == null)
                    continue;

                string @return = handler.Handle(initializer.Value, true, declarator.Symbol.Name);
                if (@return == null)
                    throw CompilerHelper.Error(initializer.Syntax, CompilationError.NoReturnValue);
                if (@return != declarator.Symbol.Name)
                    output.AppendCommand($"set {declarator.Symbol.Name} {@return}");
            }
            return null;
        }
    }
}
