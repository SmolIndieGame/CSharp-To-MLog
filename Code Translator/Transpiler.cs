using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Code_Transpiler
{
    internal class Transpiler
    {
        SyntaxTree syntaxTree;
        SemanticModel semanticModel;
        CSharpCompilation compilation;

        readonly CommandBuilder output;
        readonly Dictionary<IMethodSymbol, int> methodStartPos;
        readonly Dictionary<IMethodSymbol, int> methodIndices;
        readonly Dictionary<IParameterSymbol, int> funcArgIndices;
        readonly Dictionary<string, string> linkedBuildings;
        readonly RecursionChecker recursionChecker;
        string className;
        IMethodSymbol ctorMethod;
        IMethodSymbol entryPoint;
        IMethodSymbol currentMethod;

        OperationHandler operationHandler;

        public Transpiler()
        {
            List<MetadataReference> references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Mindustry).Assembly.Location)
            };

            var refs = Assembly.GetAssembly(typeof(Mindustry)).GetReferencedAssemblies();
            foreach (var @ref in refs)
                references.Add(MetadataReference.CreateFromFile(Assembly.Load(@ref).Location));

            compilation = CSharpCompilation.Create("Assem", null, references, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            output = new CommandBuilder(new StringBuilder());
            methodStartPos = new Dictionary<IMethodSymbol, int>();
            methodIndices = new Dictionary<IMethodSymbol, int>();
            funcArgIndices = new Dictionary<IParameterSymbol, int>();
            linkedBuildings = new Dictionary<string, string>();
            recursionChecker = new RecursionChecker();

            operationHandler = new OperationHandler(
                output,
                methodStartPos,
                methodIndices,
                funcArgIndices,
                linkedBuildings,
                AddMethodCall,
                () => currentMethod);
        }

        public void SetSource(string source)
        {
            var tmp = CSharpSyntaxTree.ParseText(source);
            if (compilation.ContainsSyntaxTree(syntaxTree))
                compilation = compilation.ReplaceSyntaxTree(syntaxTree, tmp);
            else
                compilation = compilation.AddSyntaxTrees(tmp);
            syntaxTree = tmp;

            semanticModel = compilation.GetSemanticModel(syntaxTree, false);
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

        public string Transpile()
        {
            output.Clear();
            methodStartPos.Clear();
            methodIndices.Clear();
            funcArgIndices.Clear();
            linkedBuildings.Clear();
            recursionChecker.Reset();
            ctorMethod = null;

            IEnumerable<SyntaxNode> allNodes = syntaxTree.GetCompilationUnitRoot().DescendantNodes();

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
            operationHandler.Reset(className);

            AppendCredit(@class);

            var fields = allNodes.OfType<FieldDeclarationSyntax>();
            foreach (var declaration in fields)
                HandleFieldDeclaration(declaration);

            var ctors = allNodes.OfType<ConstructorDeclarationSyntax>();
            if (ctors.Count() > 1)
                throw CompilerHelper.Error(ctors.ElementAt(1), CompilationError.TooManyConstructor);

            var ctor = ctors.FirstOrDefault();
            if (ctor != null)
            {
                currentMethod = semanticModel.GetDeclaredSymbol(ctor);
                if (ctor.ParameterList.Parameters.Count > 0)
                    throw CompilerHelper.Error(ctor, CompilationError.ParameterizedConstructor);

                methodIndices.Add(currentMethod, methodIndices.Count);
                output.AppendCommand($"set $$p{methodIndices[currentMethod]} {CompilerHelper.VarInCommand(TempValueType.Function, methodIndices[currentMethod].ToString())}");
                operationHandler.Handle(semanticModel.GetOperation((SyntaxNode)ctor.Body ?? ctor.ExpressionBody), false, null);
                output.SetValueToVarInCommand(TempValueType.Function, methodIndices[currentMethod].ToString(), output.nextLineIndex.ToString());
                ctorMethod = currentMethod;
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

            output.AppendCommand($"set $$p{methodIndices.Count} {output.nextLineIndex + 1}");
            HandleMethodDeclaration(main);
            entryPoint = currentMethod;

            foreach (var method in allMethods)
                if (method != main)
                    HandleMethodDeclaration(method);

            if (ctorMethod != null && recursionChecker.IsRecursive(ctorMethod))
                throw CompilerHelper.Error(null, CompilationError.Recursion);

            if (recursionChecker.IsRecursive(entryPoint))
                throw CompilerHelper.Error(null, CompilationError.Recursion);

            return output.ToString();
        }

        private void HandleMethodDeclaration(MethodDeclarationSyntax declaration)
        {
            currentMethod = semanticModel.GetDeclaredSymbol(declaration);
            if (!CompilerHelper.IsTypeAllowed(currentMethod.ReturnType, true))
                throw CompilerHelper.Error(declaration.ReturnType, CompilationError.UnsupportedType);
            if (currentMethod.RefKind != RefKind.None)
                throw CompilerHelper.Error(declaration.ReturnType, CompilationError.ReferenceReturn);

            for (int i = 0; i < currentMethod.Parameters.Length; i++)
            {
                if (!CompilerHelper.IsTypeAllowed(currentMethod.Parameters[i].Type))
                    throw CompilerHelper.Error(declaration.ParameterList.Parameters[i], CompilationError.UnsupportedType);
                if (!funcArgIndices.ContainsKey(currentMethod.Parameters[i]))
                    funcArgIndices.Add(currentMethod.Parameters[i], funcArgIndices.Count);
            }

            methodStartPos.Add(currentMethod, output.nextLineIndex);
            if (methodIndices.TryGetValue(currentMethod, out int index))
                output.SetValueToVarInCommand(TempValueType.Function, index.ToString(), output.nextLineIndex.ToString());
            else
                methodIndices.Add(currentMethod, methodIndices.Count);
            operationHandler.Handle(semanticModel.GetOperation(declaration.Body as SyntaxNode ?? declaration.ExpressionBody), false, null);
            if (currentMethod.ReturnType.SpecialType == SpecialType.System_Void)
                output.AppendCommand($"set @counter $$p{methodIndices[currentMethod]}");
        }

        void HandleFieldDeclaration(FieldDeclarationSyntax declaration)
        {
            var varDec = declaration.Declaration;
            if (semanticModel.GetSymbolInfo(varDec.Type).Symbol is not ITypeSymbol typeSymbol)
                throw CompilerHelper.Error(varDec, CompilationError.Unknown);
            if (typeSymbol.IsType<LinkedBuilding>())
            {
                HandleLinkedBuildingDeclaration(declaration);
                return;
            }
            if (!CompilerHelper.IsTypeAllowed(typeSymbol))
                throw CompilerHelper.Error(varDec.Type, CompilationError.UnsupportedType);

            foreach (var v in varDec.Variables)
            {
                if (semanticModel.GetDeclaredSymbol(v) is IFieldSymbol s && s.IsConst)
                    continue;
                if (v.Initializer == null)
                    continue;
                string name = $"${v.Identifier.Text}";
                string @return = operationHandler.Handle(semanticModel.GetOperation(v.Initializer.Value), true, name);
                
                if (@return == name)
                    continue;

                output.AppendCommand($"set {name} {@return}");
            }
        }

        void HandleLinkedBuildingDeclaration(FieldDeclarationSyntax declaration)
        {
            if (declaration.Modifiers.Any(SyntaxKind.ConstKeyword))
                throw CompilerHelper.Error(declaration, CompilationError.ConstLinkedBuilding);

            foreach (var v in declaration.Declaration.Variables)
            {
                if (v.Initializer == null)
                    throw CompilerHelper.Error(declaration.Declaration, CompilationError.LinkedBuildingNoInit);

                IOperation op = semanticModel.GetOperation(v.Initializer.Value);
                string @return = operationHandler.Handle(op, true, v.Identifier.Text);

                if (@return == v.Identifier.Text || op is not IInvocationOperation || !op.Type.IsType<LinkedBuilding>())
                    throw CompilerHelper.Error(declaration.Declaration, CompilationError.LinkedBuildingNoInit);

                linkedBuildings.Add(v.Identifier.Text, @return);
            }
        }

        void AddMethodCall(IMethodSymbol method)
        {
            recursionChecker.AddCall(currentMethod, method);
        }

        private void AppendCredit(ClassDeclarationSyntax @class)
        {
            List<string> credits = new List<string>();
            foreach (var attributeList in @class.AttributeLists)
                foreach (var attribute in attributeList.Attributes)
                {
                    if (semanticModel.GetSymbolInfo(attribute).Symbol.ContainingType is not ITypeSymbol s)
                        throw CompilerHelper.Error(attribute, CompilationError.Unknown);
                    if (s.IsType<CreditAttribute>())
                    {
                        ExpressionSyntax expression = attribute.ArgumentList.Arguments[0].Expression;
                        string value = CompilerHelper.GetValueFromOperation(semanticModel.GetOperation(expression));
                        if (value == null)
                            throw CompilerHelper.Error(expression, CompilationError.NotConstantValue);
                        credits.Add(value);
                    }
                    if (s.IsType<ExcludeCreditAttribute>())
                        return;
                }

            output.AppendCommand($"jump {credits.Count + 4} always");
            foreach (var credit in credits)
                output.AppendCommand($"print {credit}");
            output.AppendCommand("print \"This code is transpiled from C# code.\"");
            output.AppendCommand("print \"Check out the transpiler at\"");
            output.AppendCommand("print \"https://github.com/SmolIndieGame/CSharp-To-MLog\"");
        }
    }
}
