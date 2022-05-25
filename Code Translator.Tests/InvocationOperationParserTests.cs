using Code_Transpiler.OperationParsers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Xunit;

namespace Code_Transpiler.Tests
{
    public class InvocationOperationParserTests
    {
        InvocationOperationParser sut;
        Mock<IOperationHandler> operationHandlerMock;
        Mock<ICommandBuilder> commandBuilderMock;

        public InvocationOperationParserTests()
        {
            operationHandlerMock = new Mock<IOperationHandler>();
            commandBuilderMock = new Mock<ICommandBuilder>();
            sut = new InvocationOperationParser(operationHandlerMock.Object, commandBuilderMock.Object);
        }

        [Fact]
        public void InvokeUserDefinedMethod_ShouldCallOnMethodCalled()
        {
            int index = 1;
            int pos = 10;
            int nextLine = 2;
            string expected = @$"set $$p{index} {nextLine + 2}
jump {pos} always
";
            StringBuilder sb = new StringBuilder();
            var methodMock = new Mock<IMethodSymbol>();
            var opMock = new Mock<IInvocationOperation>();
            opMock.SetupGet(x => x.Arguments).Returns(Array.Empty<IArgumentOperation>().ToImmutableArray());
            var posDic = new Dictionary<IMethodSymbol, int>
            {
                { methodMock.Object, pos }
            };
            var indexDic = new Dictionary<IMethodSymbol, int>
            {
                { methodMock.Object, index }
            };
            operationHandlerMock.SetupGet(x => x.methodStartPos).Returns(posDic);
            operationHandlerMock.SetupGet(x => x.methodIndices).Returns(indexDic);
            commandBuilderMock.SetupGet(x => x.nextLineIndex).Returns(2);
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(x => sb.AppendLine(x));

            sut.InvokeUserDefinedMethod(opMock.Object, methodMock.Object, null);

            Assert.Equal(expected, sb.ToString());
            operationHandlerMock.Reset();
            commandBuilderMock.Reset();
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(0, 1, 2, 3, 4)]
        [InlineData(1, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(2, 0, 3, 1)]
        public void GenericCall_ShouldExecuteArgumentCorrectly(params int[] correspondParams)
        {
            string opName = "testop";
            StringBuilder expected = new StringBuilder(@$"{opName} _");
            for (int i = 0; i < correspondParams.Length; i++)
                expected.Append(i);
            expected.AppendLine();

            StringBuilder sb = new StringBuilder();
            List<IOperation> callOrder = new List<IOperation>();
            var paramList = new IParameterSymbol[correspondParams.Length];
            for (int i = 0; i < paramList.Length; i++)
                paramList[i] = new Mock<IParameterSymbol>().Object;
            var argList = new IArgumentOperation[correspondParams.Length];
            for (int i = 0; i < argList.Length; i++)
            {
                var argMock = new Mock<IArgumentOperation>();
                argMock.SetupGet(x => x.Parameter).Returns(paramList[correspondParams[i]]);
                argMock.SetupGet(x => x.Value).Returns(argMock.Object);
                argList[i] = argMock.Object;
            }

            var methodMock = new Mock<IMethodSymbol>();
            methodMock.SetupGet(x => x.Parameters).Returns(paramList.ToImmutableArray());
            var opMock = new Mock<IInvocationOperation>();
            opMock.SetupGet(x => x.Arguments).Returns(argList.ToImmutableArray());
            opMock.SetupGet(x => x.TargetMethod).Returns(methodMock.Object);
            operationHandlerMock.Setup(x => x.ToArgument(It.IsAny<IOperation>())).Callback(callOrder.Add)
                .Returns<IOperation>(op => Array.IndexOf(paramList, (op as IArgumentOperation)?.Parameter).ToString());
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(x => sb.AppendLine(x));

            sut.GenericCall(opMock.Object, opName, null);

            Assert.Equal(argList, callOrder);
            Assert.Equal(expected.ToString(), sb.ToString());
            operationHandlerMock.Reset();
            commandBuilderMock.Reset();
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(0, 1, 2, 3, 4)]
        [InlineData(1, 0)]
        [InlineData(1, 2, 0)]
        [InlineData(2, 0, 3, 1)]
        public void GenericInstanceCall_ShouldExecuteArgumentCorrectly(params int[] correspondParams)
        {
            string opName = "testop";
            string instance = "testc";
            StringBuilder expected = new StringBuilder(@$"{opName} _ {instance}");
            for (int i = 0; i < correspondParams.Length; i++)
                expected.Append(i);
            expected.AppendLine();

            StringBuilder sb = new StringBuilder();
            List<IOperation> callOrder = new List<IOperation>();
            var paramList = new IParameterSymbol[correspondParams.Length];
            for (int i = 0; i < paramList.Length; i++)
                paramList[i] = new Mock<IParameterSymbol>().Object;
            var argList = new IArgumentOperation[correspondParams.Length];
            for (int i = 0; i < argList.Length; i++)
            {
                var argMock = new Mock<IArgumentOperation>();
                argMock.SetupGet(x => x.Parameter).Returns(paramList[correspondParams[i]]);
                argMock.SetupGet(x => x.Value).Returns(argMock.Object);
                argList[i] = argMock.Object;
            }

            var instanceMock = new Mock<IOperation>();
            var methodMock = new Mock<IMethodSymbol>();
            methodMock.SetupGet(x => x.Parameters).Returns(paramList.ToImmutableArray());
            var opMock = new Mock<IInvocationOperation>();
            opMock.SetupGet(x => x.Arguments).Returns(argList.ToImmutableArray());
            opMock.SetupGet(x => x.TargetMethod).Returns(methodMock.Object);
            opMock.SetupGet(x => x.Instance).Returns(instanceMock.Object);
            operationHandlerMock.Setup(x => x.Handle(instanceMock.Object, true, It.IsAny<string>())).Returns(instance);
            operationHandlerMock.Setup(x => x.ToArgument(It.IsAny<IOperation>())).Callback(callOrder.Add)
                .Returns<IOperation>(op => Array.IndexOf(paramList, (op as IArgumentOperation)?.Parameter).ToString());
            commandBuilderMock.Setup(x => x.AppendCommand(It.IsAny<string>())).Callback<string>(x => sb.AppendLine(x));

            sut.GenericInstanceCall(opMock.Object, opName, null);

            Assert.Equal(argList, callOrder);
            Assert.Equal(expected.ToString(), sb.ToString());
            operationHandlerMock.Reset();
            commandBuilderMock.Reset();
        }
    }
}
