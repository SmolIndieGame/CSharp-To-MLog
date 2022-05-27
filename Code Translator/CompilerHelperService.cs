using Microsoft.CodeAnalysis;
using MindustryLogics;
using System;
using System.Collections.Generic;

namespace Code_Transpiler
{
    public enum TempValueType
    {
        NotJump,
        ConditionFalse,
        ConditionEnd,
        Function,
        Label,
        LoopEnd,
        LoopContinue,
        LogicAndOr
    }

    public interface ICompilerHelperService
    {
        SymbolDisplayFormat FullNameFormat { get; }

        Exception Error(SyntaxNode node, CompilationError error, params object[] args);
        string GetValueFromOperation(IOperation operation);
        bool IsTempVar(string varName);
        bool IsType<T>(string fullName);
        bool IsType<T>(ITypeSymbol symbol);
        bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false);
        string VarInCommand(TempValueType type, string index);
    }

    internal class CompilerHelperService : ICompilerHelperService
    {
        readonly HashSet<string> allowedClasses = new()
        {
            typeof(Building).FullName,
            typeof(Unit).FullName,
            typeof(Entity).FullName,
            typeof(BuildingType).FullName,
            typeof(UnitType).FullName,
            typeof(LiquidType).FullName,
            typeof(ItemType).FullName,
            typeof(InfoType).FullName,
            typeof(ControlType).FullName,
            typeof(StatusType).FullName
        };

        /// <summary>
        /// Get a temporary variable format for putting it in the command string. It will be replaced with a value later.
        /// </summary>
        public string VarInCommand(TempValueType type, string index)
        {
            return $"\"\"%{type}{index}%\"\"";
        }

        public bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false)
        {
            if (type.SpecialType == SpecialType.None)
            {
                string fullName = type.ToDisplayString(FullNameFormat);
                return type.BaseType?.SpecialType == SpecialType.System_Enum
                    || allowedClasses.Contains(fullName);
            }

            return allowVoid && type.SpecialType == SpecialType.System_Void
                || type.SpecialType == SpecialType.System_Object
                || (int)type.SpecialType >= 7 && (int)type.SpecialType <= 20 && type.SpecialType != SpecialType.System_Char;
        }

        public string GetValueFromOperation(IOperation operation)
        {
            if (!operation.ConstantValue.HasValue) return null;
            if (operation.ConstantValue.Value == null) return "null";

            if (operation.Type.BaseType?.SpecialType == SpecialType.System_Enum)
            {
                try
                {
                    var value = Convert.ToInt64(operation.ConstantValue.Value);
                    return value.ToString();
                }
                catch (OverflowException)
                {
                    throw CompilerHelper.Error(operation.Syntax, CompilationError.EnumValueTooLarge);
                }
            }

            if (operation.Type.SpecialType == SpecialType.System_Char)
                throw Error(operation.Syntax, CompilationError.CharacterType);

            if (operation.ConstantValue.Value is double d && d >= float.MaxValue)
                throw Error(operation.Syntax, CompilationError.NumericLiteralTooLarge);

            if (operation.Type.SpecialType == SpecialType.System_Boolean)
                return (bool)operation.ConstantValue.Value ? "true" : "false";

            if (operation.Type.SpecialType == SpecialType.System_String)
            {
                string value = operation.ConstantValue.Value.ToString();
                if (value.Contains('"'))
                    throw Error(operation.Syntax, CompilationError.UnsupportedCharacter, '"');
                return $"\"{value.Replace("\r", "").Replace("\n", "\\n")}\"";
            }
            return operation.ConstantValue.Value.ToString();
        }

        /// <summary>
        /// Is the variable name varName reserved for compiler usage?
        /// </summary>
        /*public bool IsReserved(string varName)
        {
            return varName == "$$r"                                                         //return
                || varName.StartsWith("$$p") && int.TryParse(varName.AsSpan(3), out _)      //pointer
                || varName.StartsWith("$$t") && int.TryParse(varName.AsSpan(3), out _)      //temporary
                || varName.StartsWith("$$a") && int.TryParse(varName.AsSpan(3), out _);     //argument
        }*/

        public bool IsTempVar(string varName)
        {
            return varName.StartsWith("$$t") && int.TryParse(varName.AsSpan(3), out _);
        }

        public SymbolDisplayFormat FullNameFormat { get; }
            = new SymbolDisplayFormat(
                memberOptions: SymbolDisplayMemberOptions.IncludeContainingType,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        public bool IsType<T>(ITypeSymbol symbol)
        {
            if (symbol is null)
                return !typeof(T).IsValueType || Nullable.GetUnderlyingType(typeof(T)) != null;
            return IsType<T>(symbol.ToDisplayString(FullNameFormat));
        }

        public bool IsType<T>(string fullName)
        {
            return fullName == typeof(T).FullName;
        }

        public Exception Error(SyntaxNode node, CompilationError error, params object[] args)
        {
            string message = $"\n{string.Format(CompilationException.GetMessage(error), args)}\n";
            if (node == null)
                return new CompilationException(message);

            FileLinePositionSpan pos = node.SyntaxTree.GetLineSpan(node.Span);
            int lineNumber = pos.StartLinePosition.Line;
            int charNumber = pos.StartLinePosition.Character;
            string str = node.SyntaxTree.GetText().ToString(node.Span);
            var newLinePos = str.IndexOf(Environment.NewLine);
            str = newLinePos > 0 ? str.Substring(0, newLinePos) : str;
            return new CompilationException($"\nAt line {lineNumber + 1}, char {charNumber + 1}: {str}\n{message}\n");
        }
    }
}
