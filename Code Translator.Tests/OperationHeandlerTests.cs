using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using MindustryLogics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Xunit;

namespace Code_Transpiler.Tests
{
    public class OperationHeandlerTests
    {
        OperationHandler sut;
        Mock<ICommandBuilder> commandBuilderMock;

        public OperationHeandlerTests()
        {
            commandBuilderMock = new Mock<ICommandBuilder>();
            sut = new OperationHandler(
                commandBuilderMock.Object,
                new Dictionary<IMethodSymbol, int>(),
                new Dictionary<IMethodSymbol, int>(),
                new Dictionary<IParameterSymbol, int>(),
                new Dictionary<string, string>(),
                null,
                null);
        }

        [Fact]
        public void Handle_ShouldReturnConstantValue()
        {
            string constString = "qwer";
            var opMock = new Mock<IOperation>();
            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(It.IsAny<IOperation>())).Returns(constString);
            CompilerHelper.Service = helperMock.Object;

            var @return = sut.Handle(opMock.Object, true, null);

            Assert.Equal(constString, @return);
        }

        

        [Fact]
        public void Handle_ShouldOutputConstantValue()
        {
            string constString = "qwer";
            string returnTo = "tmp";

            var operation = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(It.IsAny<IOperation>())).Returns(constString);
            CompilerHelper.Service = helperMock.Object;

            commandBuilderMock.Setup(x => x.AppendCommand($"set {returnTo} {constString}")).Verifiable();

            sut.Handle(operation, false, returnTo);

            commandBuilderMock.Verify();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleBinary_ShouldNotNullBeInLinedLeft()
        {
            string variable = "test";
            string nullLiteral = "null";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(variable);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(nullLiteral);
            CompilerHelper.Service = helperMock.Object;

            var ret = sut.HandleBinary(BinaryOperatorKind.NotEquals, leftOp, rightOp, true, "_");

            Assert.Equal(variable, ret);
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleBinary_ShouldNotNullBeInLinedRight()
        {
            string variable = "test";
            string nullLiteral = "null";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(nullLiteral);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(variable);
            CompilerHelper.Service = helperMock.Object;

            var ret = sut.HandleBinary(BinaryOperatorKind.NotEquals, leftOp, rightOp, true, "_");

            Assert.Equal(variable, ret);
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleJump_ShouldJump_WhenJumpIfIsTrue()
        {
            string op = "op";
            string jumpTo = "to";
            string expected = $"jump {jumpTo} notEqual {op} 0";

            var ope = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(ope)).Returns(op);
            CompilerHelper.Service = helperMock.Object;

            sut.HandleJump(ope, jumpTo, true);

            commandBuilderMock.Verify(x => x.GetNewTempVar());
            commandBuilderMock.Verify(x => x.AppendCommand(expected));
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleJump_ShouldJump_WhenJumpIfIsFalse()
        {
            string op = "op";
            string jumpTo = "to";
            string expected = $"jump {jumpTo} equal {op} 0";

            var ope = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(ope)).Returns(op);
            CompilerHelper.Service = helperMock.Object;

            sut.HandleJump(ope, jumpTo, false);

            commandBuilderMock.Verify(x => x.GetNewTempVar());
            commandBuilderMock.Verify(x => x.AppendCommand(expected));
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HandleJump_ShouldJump_WhenBinary(bool jumpIf)
        {
            string op1 = "op1";
            string op2 = "op2";
            string jumpTo = "to";
            string opCode = jumpIf ? "lessThan" : "greaterThanEq";
            string expected = $"jump {jumpTo} {opCode} {op1} {op2}";

            var ope1 = new Mock<IOperation>().Object;
            var ope2 = new Mock<IOperation>().Object;

            var opMock = new Mock<IBinaryOperation>();
            opMock.SetupGet(x => x.OperatorKind).Returns(BinaryOperatorKind.LessThan);
            opMock.SetupGet(x => x.LeftOperand).Returns(ope1);
            opMock.SetupGet(x => x.RightOperand).Returns(ope2);
            var op = opMock.Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(ope1)).Returns(op1);
            helperMock.Setup(x => x.GetValueFromOperation(ope2)).Returns(op2);
            CompilerHelper.Service = helperMock.Object;

            sut.HandleJump(op, jumpTo, jumpIf);

            commandBuilderMock.Verify(x => x.GetNewTempVar());
            commandBuilderMock.Verify(x => x.AppendCommand(expected));
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleJump_ShouldInvert_WhenUnaryNot()
        {
            string op = "op";
            string jumpTo = "to";
            string expected = $"jump {jumpTo} equal {op} 0";

            var opIn = new Mock<IOperation>().Object;

            var opMock = new Mock<IUnaryOperation>();
            opMock.SetupGet(x => x.OperatorKind).Returns(UnaryOperatorKind.Not);
            opMock.SetupGet(x => x.Operand).Returns(opIn);
            var ope = opMock.Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(opIn)).Returns(op);
            CompilerHelper.Service = helperMock.Object;

            sut.HandleJump(ope, jumpTo, true);

            commandBuilderMock.Verify(x => x.GetNewTempVar());
            commandBuilderMock.Verify(x => x.AppendCommand(expected));
            commandBuilderMock.VerifyNoOtherCalls();
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleLogicAndOr_ShouldChainJump_WhenJumpIfFalseAndIsAnd()
        {
            string op1 = "op1";
            string op2 = "op2";
            string expected = @"jump 0 equal op1 0
jump 0 equal op2 0
";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(op1);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(op2);
            CompilerHelper.Service = helperMock.Object;

            StringBuilder sb = new StringBuilder();
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(s => sb.AppendLine(s));

            sut.HandleLogicAndOr(leftOp, rightOp, "0", true, false);
            Assert.Equal(expected, sb.ToString());
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleLogicAndOr_ShouldChainJump_WhenJumpIfTrueAndIsOr()
        {
            string op1 = "op1";
            string op2 = "op2";
            string expected = @"jump 0 notEqual op1 0
jump 0 notEqual op2 0
";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(op1);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(op2);
            CompilerHelper.Service = helperMock.Object;

            StringBuilder sb = new StringBuilder();
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(s => sb.AppendLine(s));

            sut.HandleLogicAndOr(leftOp, rightOp, "0", false, true);
            Assert.Equal(expected, sb.ToString());
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleLogicAndOr_ShouldElseJump_WhenJumpIfTrueAndIsAnd()
        {
            string jumpTo = "to";
            string notJump = "else";
            string op1 = "op1";
            string op2 = "op2";
            string expected = @$"jump {notJump} equal {op1} 0
jump {notJump} equal {op2} 0
jump {jumpTo} always
";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(op1);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(op2);
            helperMock.Setup(x => x.VarInCommand(TempValueType.NotJump, "1")).Returns(notJump);
            CompilerHelper.Service = helperMock.Object;

            StringBuilder sb = new StringBuilder();
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(s => sb.AppendLine(s));

            sut.HandleLogicAndOr(leftOp, rightOp, jumpTo, true, true);
            Assert.Equal(expected, sb.ToString());
            commandBuilderMock.Reset();
        }

        [Fact]
        public void HandleLogicAndOr_ShouldElseJump_WhenJumpIfFalseAndIsOr()
        {
            string jumpTo = "to";
            string notJump = "else";
            string op1 = "op1";
            string op2 = "op2";
            string expected = @$"jump {notJump} notEqual {op1} 0
jump {notJump} notEqual {op2} 0
jump {jumpTo} always
";

            var leftOp = new Mock<IOperation>().Object;
            var rightOp = new Mock<IOperation>().Object;

            var helperMock = new Mock<ICompilerHelperService>();
            helperMock.Setup(x => x.GetValueFromOperation(leftOp)).Returns(op1);
            helperMock.Setup(x => x.GetValueFromOperation(rightOp)).Returns(op2);
            helperMock.Setup(x => x.VarInCommand(TempValueType.NotJump, "1")).Returns(notJump);
            CompilerHelper.Service = helperMock.Object;

            StringBuilder sb = new StringBuilder();
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(s => sb.AppendLine(s));

            sut.HandleLogicAndOr(leftOp, rightOp, jumpTo, false, false);
            Assert.Equal(expected, sb.ToString());
            commandBuilderMock.Reset();
        }
    }
}