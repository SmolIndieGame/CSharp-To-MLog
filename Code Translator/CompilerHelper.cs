using Microsoft.CodeAnalysis;
using MindustryLogics;
using System;
using System.Collections.Generic;

namespace Code_Transpiler
{
    public static class CompilerHelper
    {
        public static ICompilerHelperService Service { get; set; } = new CompilerHelperService();
        /// <summary>
        /// Get a temporary variable format for putting it in the command string. It will be replaced with a value later.
        /// </summary>
        public static string VarInCommand(TempValueType type, string index) => Service.VarInCommand(type, index);
        public static bool IsTypeEnum(this ITypeSymbol type) => Service.IsTypeEnum(type);
        public static bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false) => Service.IsTypeAllowed(type, allowVoid);
        public static string GetValueFromOperation(IOperation operation) => Service.GetValueFromOperation(operation);
        public static int? GetIntValueFromEnumLiteral(IOperation operation) => Service.GetIntValueFromEnumLiteral(operation);
        public static bool IsTempVar(string varName) => Service.IsTempVar(varName);
        public static SymbolDisplayFormat FullNameFormat => Service.FullNameFormat;
        public static bool IsType<T>(this ITypeSymbol symbol) => Service.IsType<T>(symbol);
        public static bool IsType<T>(string fullName) => Service.IsType<T>(fullName);
        public static Exception Error(SyntaxNode node, CompilationError error, params object[] args) => Service.Error(node, error, args);
    }
}
