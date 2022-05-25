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
        int? GetIntValueFromEnumLiteral(IOperation operation);
        string GetValueFromOperation(IOperation operation);
        bool IsTempVar(string varName);
        bool IsType<T>(string fullName);
        bool IsType<T>(ITypeSymbol symbol);
        bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false);
        bool IsTypeEnum(ITypeSymbol type);
        string VarInCommand(TempValueType type, string index);
    }

    internal class CompilerHelperService : ICompilerHelperService
    {
        readonly HashSet<Type> typeEnums = new HashSet<Type>
        {
            typeof(BuildingType),
            typeof(UnitType),
            typeof(LiquidType),
            typeof(ItemType),
            typeof(InfoType),
            typeof(ControlType),
            typeof(StatusType)
        };

        /// <summary>
        /// Get a temporary variable format for putting it in the command string. It will be replaced with a value later.
        /// </summary>
        public string VarInCommand(TempValueType type, string index)
        {
            return $"\"\"%{type}{index}%\"\"";
        }

        public bool IsTypeEnum(ITypeSymbol type)
        {
            if (type == null || type.SpecialType != SpecialType.None) return false;
            string fullName = type.ToDisplayString(FullNameFormat);
            foreach (var item in typeEnums)
                if (fullName == item.FullName)
                    return true;
            return false;
        }

        public bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false)
        {
            if (type.SpecialType == SpecialType.None)
            {
                string fullName = type.ToDisplayString(FullNameFormat);
                return fullName == typeof(Building).FullName
                    || fullName == typeof(Unit).FullName
                    || fullName == typeof(Entity).FullName
                    || IsTypeEnum(type);
            }

            return allowVoid && type.SpecialType == SpecialType.System_Void
                || type.SpecialType == SpecialType.System_Object
                || (int)type.SpecialType >= 7 && (int)type.SpecialType <= 20 && type.SpecialType != SpecialType.System_Char;

            /*switch (specialType)
            {
                case SpecialType.System_Enum:
                case SpecialType.System_Boolean:
                case SpecialType.System_Char:
                case SpecialType.System_SByte:
                case SpecialType.System_Byte:
                case SpecialType.System_Int16:
                case SpecialType.System_UInt16:
                case SpecialType.System_Int32:
                case SpecialType.System_UInt32:
                case SpecialType.System_Int64:
                case SpecialType.System_UInt64:
                case SpecialType.System_Decimal:
                case SpecialType.System_Single:
                case SpecialType.System_Double:
                case SpecialType.System_String:
                    return true;
                default:
                    return false;
            }*/
        }

        public string GetValueFromOperation(IOperation operation)
        {
            if (!operation.ConstantValue.HasValue) return null;

            if (operation.Type.SpecialType == SpecialType.None)
            {
                if (operation.ConstantValue.Value is null) return "null";

                string fullName = operation.Type.ToDisplayString(FullNameFormat);
                if (fullName == typeof(InfoType).FullName)
                    return GetCamelValueFromEnum(typeof(InfoType), operation.ConstantValue.Value);
                if (fullName == typeof(ControlType).FullName)
                    return GetCamelValueFromEnum(typeof(ControlType), operation.ConstantValue.Value);
                foreach (var item in typeEnums)
                    if (fullName == item.FullName)
                        return GetTypeValueFromEnum(item, operation.ConstantValue.Value);
                throw Error(operation.Syntax, CompilationError.UnsupportedType);
            }

            if (operation.Type.SpecialType == SpecialType.System_Char)
                throw Error(operation.Syntax, CompilationError.CharacterType);

            if (operation.ConstantValue.Value is double d && d >= float.MaxValue)
                throw Error(operation.Syntax, CompilationError.NumericLiteralTooLarge);

            if (operation.Type.SpecialType == SpecialType.System_Boolean)
                return (bool)operation.ConstantValue.Value ? "true" : "false";

            if (operation.Type.SpecialType == SpecialType.System_String && operation.ConstantValue.Value != null)
            {
                string value = operation.ConstantValue.Value.ToString();
                if (value.Contains('"'))
                    throw Error(operation.Syntax, CompilationError.UnsupportedCharacter, '"');
                return $"\"{value.Replace("\r", "").Replace("\n", "\\n")}\"";
            }
            return operation.ConstantValue.Value?.ToString() ?? "null";
        }

        public int? GetIntValueFromEnumLiteral(IOperation operation)
        {
            if (!operation.ConstantValue.HasValue || operation.Type.SpecialType != SpecialType.None)
                return null;

            object v = operation.ConstantValue.Value;
            if (v is int i)
                return i;
            if (v is uint ui)
                return (int)ui;
            return null;
        }

        string GetTypeValueFromEnum(Type enumType, object value)
        {
            string val = Enum.GetName(enumType, value);
            if (Convert.ToInt32(value) == 0 || val == null)
                return "null";
            return "@" + val.ToLower().Replace('_', '-');
        }

        string GetCamelValueFromEnum(Type enumType, object value)
        {
            string val = Enum.GetName(enumType, value);
            if (Convert.ToInt32(value) == 0 || val == null)
                return "null";
            return $"@{char.ToLower(val[0])}{val.Substring(1)}";
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
