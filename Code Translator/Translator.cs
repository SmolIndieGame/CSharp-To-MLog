using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Code_Translator
{
    internal class Translator
    {
        readonly SyntaxTree syntaxTree;
        readonly CompilationUnitSyntax treeRoot;
        readonly SemanticModel semanticModel;
        readonly CSharpCompilation compilation;

        readonly CommandBuilder output;
        readonly Dictionary<IMethodSymbol, int> methodStartPos;
        readonly Dictionary<IMethodSymbol, int> methodIndices;
        readonly Dictionary<IParameterSymbol, int> funcArgIndices;
        readonly RecursionChecker recursionChecker;
        string className;
        IMethodSymbol entryPoint;
        IMethodSymbol currentMethod;

        OperationHandler operationHandler;

        public Translator(string source)
        {
            syntaxTree = CSharpSyntaxTree.ParseText(source);
            treeRoot = syntaxTree.GetCompilationUnitRoot();
            compilation = CSharpCompilation.Create("Assem")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(Mindustry).Assembly.Location))
                .AddSyntaxTrees(syntaxTree)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var refs = Assembly.GetAssembly(typeof(Mindustry)).GetReferencedAssemblies();
            compilation = compilation.AddReferences(refs.Select(n => MetadataReference.CreateFromFile(Assembly.Load(n).Location)));
            semanticModel = compilation.GetSemanticModel(syntaxTree);

            output = new CommandBuilder(new StringBuilder());
            methodStartPos = new Dictionary<IMethodSymbol, int>();
            methodIndices = new Dictionary<IMethodSymbol, int>();
            funcArgIndices = new Dictionary<IParameterSymbol, int>();
            recursionChecker = new RecursionChecker();
        }

        public bool CheckCodeValidity()
        {
            var dg = compilation.GetDiagnostics();
            bool returns = true;
            foreach (Diagnostic d in dg)
            {
                if (d.Severity != DiagnosticSeverity.Error)
                    continue;

                if (returns)
                {
                    Console.WriteLine("\nThe following compile error occured:");
                    returns = false;
                }
                Console.WriteLine(d.GetMessage());
            }
            return returns;
        }

        public string Translate()
        {
            output.Clear();
            methodStartPos.Clear();
            methodIndices.Clear();
            funcArgIndices.Clear();

            //if (treeRoot.Usings.Count != 1 || treeRoot.Usings.First().Name.ToString() != "MindustryLogics")
            //    throw CompilerHelper.Error(null, CompilationError.UnsupportedUsings);

            IEnumerable<SyntaxNode> allNodes = treeRoot.DescendantNodes();

            var classes = allNodes.OfType<ClassDeclarationSyntax>();
            if (classes.Count() > 1)
                throw CompilerHelper.Error(classes.ElementAt(1), CompilationError.TooManyClasses);

            var @class = classes.First();
            className = @class.Identifier.Text;
            INamedTypeSymbol classSymbol = semanticModel.GetDeclaredSymbol(@class);
            if (!classSymbol.ContainingNamespace.IsGlobalNamespace)
                throw CompilerHelper.Error(@class, CompilationError.WithinNamespace);
            if (classSymbol.BaseType != null && !classSymbol.BaseType.IsType<object>())
                throw CompilerHelper.Error(@class, CompilationError.InheritClasses);
            if (@class.Modifiers.Count > 0)
                throw CompilerHelper.Error(@class, CompilationError.ClassModifier, className);

            var fields = allNodes.OfType<FieldDeclarationSyntax>();
            var declarations = fields.Select(n => n.Declaration);
            foreach (var declaration in declarations)
                HandleFieldDeclaration(declaration);

            var ctors = allNodes.OfType<ConstructorDeclarationSyntax>();
            if (ctors.Count() > 1)
                throw CompilerHelper.Error(@class, CompilationError.TooManyConstructor);

            operationHandler = new OperationHandler(
                semanticModel,
                output,
                methodStartPos,
                methodIndices,
                funcArgIndices,
                className,
                AddMethodCall);

            AppendCredit();

            var ctor = ctors.FirstOrDefault();
            if (ctor != null)
            {
                if (ctor.ParameterList.Parameters.Count > 0)
                    throw CompilerHelper.Error(ctor, CompilationError.ParameterizedConstructor);

                operationHandler.Handle(semanticModel.GetOperation((SyntaxNode)ctor.Body ?? ctor.ExpressionBody), false, null);
            }

            var allMethods = allNodes.OfType<MethodDeclarationSyntax>();
            var mains = allMethods.Where(method => method.Identifier.Text == "Main");
            if (mains.Count() > 1)
                throw CompilerHelper.Error(mains.ElementAt(1), CompilationError.TooManyMainEntry);

            var main = mains.FirstOrDefault();
            if (main == null)
                throw CompilerHelper.Error(@class, CompilationError.NoMainEntry);
            if (main.ParameterList.Parameters.Count > 0)
                throw CompilerHelper.Error(main, CompilationError.ParameterizedMainEntry);

            output.AppendCommand($"set ptr {output.nextLineIndex + 1}");
            HandleMethodDeclaration(main);
            entryPoint = currentMethod;

            foreach (var method in allMethods)
                if (method != main)
                    HandleMethodDeclaration(method);

            if (recursionChecker.IsRecursive(entryPoint))
                throw CompilerHelper.Error(null, CompilationError.Recursion);

            return output.ToString();
        }

        private void HandleMethodDeclaration(MethodDeclarationSyntax declaration)
        {
            currentMethod = semanticModel.GetDeclaredSymbol(declaration);
            if (!CompilerHelper.IsTypeAllowed(currentMethod.ReturnType, true))
                throw CompilerHelper.Error(declaration, CompilationError.UnsupportedType);
            
            foreach (var param in currentMethod.Parameters)
            {
                if (!CompilerHelper.IsTypeAllowed(param.Type))
                    throw CompilerHelper.Error(declaration, CompilationError.UnsupportedType);
                if (param.RefKind != RefKind.None)
                    throw CompilerHelper.Error(declaration, CompilationError.ReferenceParameter);
                if (!funcArgIndices.ContainsKey(param))
                    funcArgIndices.Add(param, funcArgIndices.Count);
            }

            methodStartPos.Add(currentMethod, output.nextLineIndex);
            if (methodIndices.TryGetValue(currentMethod, out int index))
                output.SetValueToVarInCommand(TempValueType.Function, index.ToString(), output.nextLineIndex.ToString());
            //w w = new w();
            //w.Visit(semanticModel.GetOperation(declaration.Body as SyntaxNode ?? declaration.ExpressionBody));
            operationHandler.Handle(semanticModel.GetOperation(declaration.Body as SyntaxNode ?? declaration.ExpressionBody), false, null);
            if (currentMethod.ReturnType.SpecialType == SpecialType.System_Void)
                output.AppendCommand($"set @counter ptr");
        }

        void HandleFieldDeclaration(VariableDeclarationSyntax declaration)
        {
            if (semanticModel.GetSymbolInfo(declaration.Type).Symbol is not ITypeSymbol typeSymbol)
                throw CompilerHelper.Error(declaration, CompilationError.Unknown);
            if (!CompilerHelper.IsTypeAllowed(typeSymbol))
                throw CompilerHelper.Error(declaration.Type, CompilationError.UnsupportedType);

            foreach (var v in declaration.Variables)
            {
                if (semanticModel.GetDeclaredSymbol(v) is IFieldSymbol s && s.IsConst)
                    continue;
                if (v.Initializer != null)
                    throw CompilerHelper.Error(declaration, CompilationError.FieldInitialized);
                //output.AppendCommand($"set _{v.Identifier.Text} null");
            }
        }

        void AddMethodCall(IMethodSymbol method)
        {
            recursionChecker.AddCall(currentMethod, method);
        }

        void AppendCredit()
        {
            output.AppendCommand("jump 4 always");
            output.AppendCommand("print \"This code is translated from C# code.\"");
            output.AppendCommand("print \"Check out the translator at\"");
            output.AppendCommand("print \"https://github.com/SmolIndieGame/CSharp-To-MLog\"");
        }
    }
}
