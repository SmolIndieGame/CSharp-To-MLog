using Moq;
using System;
using System.Linq.Expressions;
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
            commandBuilderMock = new Mock<ICommandBuilder>(new StringBuilder());
            sut = new OperationHandler(commandBuilderMock.Object, new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IMethodSymbol, int>(), new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IMethodSymbol, int>(), new System.Collections.Generic.Dictionary<Microsoft.CodeAnalysis.IParameterSymbol, int>(), null);
        }

        [Fact]
        public void Test1()
        {
            
        }
    }
}