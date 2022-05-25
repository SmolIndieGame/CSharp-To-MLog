using Code_Transpiler.OperationParsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Code_Transpiler.Tests
{
    public class BinaryOperationParserTests
    {
        BinaryOperationParser sut;
        Mock<IOperationHandler> operationHandlerMock;
        Mock<ICommandBuilder> commandBuilderMock;

        public BinaryOperationParserTests()
        {
            operationHandlerMock = new Mock<IOperationHandler>();
            commandBuilderMock = new Mock<ICommandBuilder>();
            sut = new BinaryOperationParser(operationHandlerMock.Object, commandBuilderMock.Object);
        }

        [Theory]
        [InlineData(BinaryOperatorKind.ConditionalAnd)]
        [InlineData(BinaryOperatorKind.ConditionalOr)]
        public void ShouldCallJump_WhenConditionalAndOr(BinaryOperatorKind opKind)
        {
            string returnTo = "ret";

            var op1 = new Mock<IOperation>().Object;
            var op2 = new Mock<IOperation>().Object;
            
            var opMock = new Mock<IBinaryOperation>();
            opMock.SetupGet(x => x.OperatorKind).Returns(opKind);
            opMock.SetupGet(x => x.LeftOperand).Returns(op1);
            opMock.SetupGet(x => x.RightOperand).Returns(op2);
            var op = opMock.Object;

            sut.Parse(op, true, returnTo);

            operationHandlerMock.Verify(x => x.HandleJump(op, It.IsAny<string>(), false));
            operationHandlerMock.VerifyNoOtherCalls();
            operationHandlerMock.Reset();
        }
    }
}
