using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;

namespace Code_Transpiler
{
    public interface IOperationHandler
    {
        Dictionary<IMethodSymbol, int> methodStartPos { get; }
        Dictionary<IMethodSymbol, int> methodIndices { get; }
        Dictionary<IParameterSymbol, int> funcArgIndices { get; }
        
        string className { get; }

        Dictionary<string, int> labelPos { get; }
        int currentLoopIndent { get; set; }

        string Handle(IOperation operation, bool canBeInline, in string returnToVar);
        string HandleBinary(BinaryOperatorKind operatorKind, IOperation leftOperand, IOperation rightOperand, bool canBeInline, in string returnToVar);
        void HandleJump(IOperation condition, string jumpToLine, bool jumpIf);
        void HandleLogicAndOr(IOperation leftOperand, IOperation rightOperand, string jumpTo, bool andOr, bool jumpIf);
        void OnMethodCalled(IMethodSymbol callee);
        string ToArgument(IOperation operation);
    }
}