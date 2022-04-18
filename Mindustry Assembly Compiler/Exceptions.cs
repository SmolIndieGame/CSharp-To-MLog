using System;

namespace Mindustry_Assembly_Compiler
{
    public enum CompilationError
    {
        UnsupportedStatement,
        UnsupportedExpression,
        UnsupportedOperation,
        UnsupportedInvocation,
        UnsupportedType,
        CharacterType,
        ReservedVarName,
        NumericLiteralTooLarge,
        NoReturnValue,
        TooManyClasses,
        TooManyConstructor,
        ParameterizedConstructor,
        NoMainEntry,
        TooManyMainEntry,
        ParameterizedMainEntry,
        InvalidAssignment,
        FieldInitialized,
        LabelDoesNotExists,
        UnsupportedCharacter,
        NotConstantValue,
        ReferencedProperty,
        Recursion,
        StringAddition,
        NoneEnumLiteral,
        WithinNamespace,
        InheritClasses,
        CastingEnum,
        InvalidLinkIndex,
        TooManyConstraints,
        NullLiteral,
        ReferenceParameter,
        ClassModifier,
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
                CompilationError.ReservedVarName => "the variable name {0} is reserved for the compiler, please use another variable name.",
                CompilationError.NumericLiteralTooLarge => "Mindustry does not support double precision numeric literal for some reason, use Operation to create big numbers.",
                CompilationError.NoReturnValue => "This operation has no return value.",
                CompilationError.TooManyClasses => "Only one class is allowed",
                CompilationError.InvalidAssignment => "Invalid assignment.",
                CompilationError.Unknown => "Something went wrong.",
                CompilationError.TooManyConstructor => "Only one constructor is allowed.",
                CompilationError.ParameterizedConstructor => "The constructor has to be parameterless",
                CompilationError.FieldInitialized => "Please initialize fields inside the constructor.",
                CompilationError.NoMainEntry => "Require method: void Main().",
                CompilationError.TooManyMainEntry => "Only one main entry is allowed.",
                CompilationError.ParameterizedMainEntry => "The main entry has to be parameterless.",
                CompilationError.LabelDoesNotExists => "The label {0} does not exists.",
                CompilationError.UnsupportedCharacter => "The character {0} is unsupported.",
                CompilationError.Recursion => "Recursion is unsupported, you can use a cell to mimic recursive behaviour.",
                CompilationError.NotConstantValue => "Only constant value is allowed here.",
                CompilationError.ReferencedProperty => "User defined property is unsupported, use method instead.",
                CompilationError.StringAddition => "Mindustry does not support string addition.",
                CompilationError.NoneEnumLiteral => "Enum value of None is not allowed here.",
                CompilationError.WithinNamespace => "The class to compile must not be contained in a namespace.",
                CompilationError.InheritClasses => $"The class to compile must inherit System.Object.",
                CompilationError.CastingEnum => "Enum that represents types cannot be cast to numbers.",
                CompilationError.InvalidLinkIndex => "Link index out of bound. (link index starts at 1)",
                CompilationError.TooManyConstraints => "Too many constraints, only a maximum of three constraints are allowed.",
                CompilationError.NullLiteral => "Null is not allowed here.",
                CompilationError.ReferenceParameter => "Reference parameter in user defined method is currently unsupported.",
                CompilationError.ClassModifier => "Modifiers for the class to compile is unsupported, declare the class as:\nclass {0} {{ }}",
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
