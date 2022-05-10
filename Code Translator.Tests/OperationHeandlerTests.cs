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

namespace Code_Translator.Tests
{
    public class OperationHeandlerTests
    {
        OperationHandler sut;
        Mock<ICommandBuilder> commandBuilderMock;

        public OperationHeandlerTests()
        {
            commandBuilderMock = new Mock<ICommandBuilder>();
            sut = new OperationHandler(commandBuilderMock.Object, new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IMethodSymbol, int>(), new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IMethodSymbol, int>(), new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IParameterSymbol, int>(), null);
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