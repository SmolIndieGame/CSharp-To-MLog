using Microsoft.CodeAnalysis;
using MindustryLogics;
using System;
using System.Collections.Generic;

namespace Code_Translator
{
    public enum TempValueType
    {
        Condition1,
        Condition2,
        Function,
        Label,
        LoopEnd,
        LoopContinue
    }

    public static class CompilerHelper
    {
        static readonly HashSet<Type> typeEnums = new HashSet<Type>
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
        public static string VarInCommand(TempValueType type, string index)
        {
            return $"\"\"${type}{index}\"\"";
        }

        public static bool IsTypeEnum(this ITypeSymbol type)
        {
            if (type == null || type.SpecialType != SpecialType.None) return false;
            string fullName = type.ToDisplayString(FullNameFormat);
            foreach (var item in typeEnums)
                if (fullName == item.FullName)
                    return true;
            return false;
        }

        public static bool IsTypeAllowed(ITypeSymbol type, bool allowVoid = false)
        {
            if (type.SpecialType == SpecialType.None)
            {
                string fullName = type.ToDisplayString(FullNameFormat);
                return fullName == typeof(Building).FullName
                    || fullName == typeof(Unit).FullName
                    || fullName == typeof(Entity).FullName
                    || type.IsTypeEnum();
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

        public static string GetValueFromOperation(IOperation operation)
        {
            if (!operation.ConstantValue.HasValue) return null;

            if (operation.Type.SpecialType == SpecialType.None)
            {
                if (operation.ConstantValue.Value is null) return "null";

                string fullName = operation.Type.ToDisplayString(FullNameFormat);
                if (fullName == typeof(InfoType).FullName)
                    return GetInfoTypeValue((InfoType)operation.ConstantValue.Value);
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

            if (operation.Type.SpecialType == SpecialType.System_String)
            {
                string value = operation.ConstantValue.Value.ToString();
                if (value.Contains('"'))
                    throw Error(operation.Syntax, CompilationError.UnsupportedCharacter, '"');
                return $"\"{value.Replace("\r", "").Replace("\n", "\\n").Replace("[", "[[")}\"";
            }
            return operation.ConstantValue.Value?.ToString() ?? "null";
        }

        public static int? GetIntValueFromEnumLiteral(IOperation operation)
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

        static string GetTypeValueFromEnum(Type enumType, object value)
        {
            if (Convert.ToInt32(value) == 0)
                return "null";
            return "@" + Enum.GetName(enumType, value).ToLower().Replace('_', '-');
        }

        static string GetInfoTypeValue(InfoType @enum)
        {
            if (@enum == InfoType.None)
                return "0";
            string name = @enum.ToString();
            return $"@{char.ToLower(name[0])}{name.Substring(1)}";
        }

        /// <summary>
        /// Is the variable name varName reserved for compiler usage?
        /// </summary>
        public static bool IsReserved(string varName)
        {
            return varName == "ret" || varName == "ptr"
                || varName.StartsWith("tmp") && int.TryParse(varName.AsSpan(3), out _)
                || varName.StartsWith("arg") && int.TryParse(varName.AsSpan(3), out _);
        }

        public static bool IsTempVar(string varName)
        {
            return varName.StartsWith("tmp") && int.TryParse(varName.AsSpan(3), out _);
        }

        public static SymbolDisplayFormat FullNameFormat { get; }
            = new SymbolDisplayFormat(
                memberOptions: SymbolDisplayMemberOptions.IncludeContainingType,
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

        public static bool IsType<T>(this ITypeSymbol symbol)
        {
            if (symbol is null)
                return !typeof(T).IsValueType || Nullable.GetUnderlyingType(typeof(T)) != null;
            return IsType<T>(symbol.ToDisplayString(FullNameFormat));
        }

        public static bool IsType<T>(string fullName)
        {
            return fullName == typeof(T).FullName;
        }

        public static Exception Error(SyntaxNode node, CompilationError error, params object[] args)
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
