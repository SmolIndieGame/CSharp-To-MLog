using System;

namespace Code_Transpiler
{
    public enum CompilationError
    {
        UnsupportedStatement,
        UnsupportedExpression,
        UnsupportedOperation,
        UnsupportedInvocation,
        UnsupportedType,
        CharacterType,
        NumericLiteralTooLarge,
        NoReturnValue,
        TooManyClasses,
        TooManyConstructor,
        ParameterizedConstructor,
        NoMainEntry,
        TooManyMainEntry,
        ParameterizedMainEntry,
        UnsupportedCharacter,
        NotConstantValue,
        ReferencedProperty,
        Recursion,
        StringAddition,
        WithinNamespace,
        InheritClasses,
        InvalidLinkIndex,
        TooManyConstraints,
        NullLiteral,
        ReferenceReturn,
        ClassModifier,
        SetLinkedBuilding,
        DefineLinkedBuilding,
        InvalidArgument,
        EnumValueTooLarge,
        CastingLinkedBuilding,
        LinkedBuildingNoInit,
        ConstLinkedBuilding,
        Unknown,
    }

    [Serializable]
    public class CompilationException : Exception
    {
        public static string GetMessage(CompilationError error)
        {
            return error switch
            {
                CompilationError.UnsupportedStatement => "Unsupported statement.",
                CompilationError.UnsupportedExpression => "Unsupported expression.",
                CompilationError.UnsupportedOperation => "Unsupported operation: {0}.",
                CompilationError.UnsupportedInvocation => "Unsupported invocation.",
                CompilationError.UnsupportedType => "Unsupported type.",
                CompilationError.CharacterType => "char is not supported, use string instead.",
                CompilationError.NumericLiteralTooLarge => "Mindustry does not support double precision numeric literal for some reason, use Operation to create big numbers.",
                CompilationError.NoReturnValue => "This operation has no return value.",
                CompilationError.TooManyClasses => "Only one class is allowed",
                CompilationError.Unknown => "Something went wrong.",
                CompilationError.TooManyConstructor => "Only one constructor is allowed.",
                CompilationError.ParameterizedConstructor => "The constructor has to be parameterless",
                CompilationError.NoMainEntry => "Require method: void Main().",
                CompilationError.TooManyMainEntry => "Only one main entry is allowed.",
                CompilationError.ParameterizedMainEntry => "The main entry has to be parameterless.",
                CompilationError.UnsupportedCharacter => "The character {0} is unsupported.",
                CompilationError.Recursion => "Recursion is unsupported, you can use a cell to mimic recursive behaviour.",
                CompilationError.NotConstantValue => "Only constant value is allowed here.",
                CompilationError.ReferencedProperty => "User defined property is unsupported, use method instead.",
                CompilationError.StringAddition => "Mindustry does not support string addition.",
                CompilationError.WithinNamespace => "The class to transpile must not be contained in a namespace.",
                CompilationError.InheritClasses => "The class to transpile must inherit System.Object.",
                CompilationError.InvalidLinkIndex => "Link index out of bound. (link index starts at 1)",
                CompilationError.TooManyConstraints => "Too many constraints, only a maximum of three constraints are allowed.",
                CompilationError.NullLiteral => "Null is not allowed here.",
                CompilationError.ReferenceReturn => "Reference returns is currently unsupported.",
                CompilationError.ClassModifier => "Modifiers for the class to transpile is unsupported, declare the class as:\nclass {0} {{ }}",
                CompilationError.SetLinkedBuilding => "Linked buildings cannot be set.",
                CompilationError.DefineLinkedBuilding => "Linked buildings cannot be defined as a local variable, define it as a field instead.",
                CompilationError.InvalidArgument => "This value cannot be passed into the method {0}.",
                CompilationError.EnumValueTooLarge => "The underlying value of this enum value is too large for Mindustry to handle, please change it to a smaller value.",
                CompilationError.CastingLinkedBuilding => "Buildings cannot be casted to linked buildings.",
                CompilationError.LinkedBuildingNoInit => "Initialize the field with GetLink(BuildingType, int).\nExample: LinkedBuilding duo = GetLink(BuildingType.Duo, 1);",
                CompilationError.ConstLinkedBuilding => "Linked buildings cannot be declared as a constant.",
                _ => error.ToString(),
            };
        }

        public CompilationException() : base("Compilation failed.") { }
        public CompilationException(string message) : base(message) { }
        public CompilationException(string message, Exception inner) : base(message, inner) { }
        protected CompilationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
