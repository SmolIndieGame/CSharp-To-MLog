/*
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Code_Transpiler.OperationParsers;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.InvocationParsers
{
    public class GetIconParser : InvocationParserBase
    {
        public GetIconParser(IOperationHandler handler, IInvocationOperationParser operationParser, ICommandBuilder output) : base(handler, operationParser, output)
        {
        }

        protected override Type methodContainingType => typeof(Mindustry);
        protected override string methodName => throw new NotImplementedException();

        public override string Parse(IInvocationOperation operation, bool canBeInline, in string returnToVar)
        {
            throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedInvocation);
            uint tmp;
            Span<byte> bytes = stackalloc byte[2];

            IArgumentOperation argOp = operation.Arguments[0];
            string fullName = argOp.Value.Type.ToDisplayString(CompilerHelper.FullNameFormat);
            int? val = CompilerHelper.GetIntValueFromEnumLiteral(argOp.Value);
            if (!val.HasValue)
                throw CompilerHelper.Error(argOp.Syntax, CompilationError.NotConstantValue);
            if (val == 0)
                throw CompilerHelper.Error(argOp.Syntax, CompilationError.NoneEnumLiteral);
            if (fullName == typeof(ItemType).FullName)
            {
                tmp = 0b11111000_00111001u - (uint)val.Value;
                bytes[0] = (byte)(tmp >> 8);
                bytes[1] = (byte)((bytes[0] << 8) ^ tmp);
                return $"\"{Encoding.BigEndianUnicode.GetString(bytes)}\"";
            }
            if (fullName == typeof(LiquidType).FullName)
            {
                tmp = 0b11111000_00101001u - (uint)val.Value;
                bytes[0] = (byte)(tmp >> 8);
                bytes[1] = (byte)((bytes[0] << 8) ^ tmp);
                return $"\"{Encoding.BigEndianUnicode.GetString(bytes)}\"";
            }

            tmp = (uint)val.Value;
            bytes[0] = (byte)(tmp >> 8);
            bytes[1] = (byte)((bytes[0] << 8) ^ tmp);
            return $"\"{Encoding.BigEndianUnicode.GetString(bytes)}\"";

            throw CompilerHelper.Error(argOp.Syntax, CompilationError.Unknown);
        }
    }
}
*/