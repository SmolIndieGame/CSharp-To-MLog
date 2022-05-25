using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using MindustryLogics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Transpiler.OperationParsers
{
    public class PropertyReferenceParser : OperationParserBase<IPropertyReferenceOperation>
    {
        public PropertyReferenceParser(IOperationHandler handler, ICommandBuilder output) : base(handler, output)
        {
        }

        public override string Parse(IPropertyReferenceOperation operation, bool canBeInline, in string returnToVar)
        {
            INamedTypeSymbol containingType = operation.Property.ContainingType;
            if (containingType.Name == handler.className)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.ReferencedProperty);

            if (returnToVar == null) return output.GetNewTempVar();

            if (containingType == null)
                throw CompilerHelper.Error(operation.Syntax, CompilationError.Unknown);

            string @return = GetContentType(operation.Property, containingType);
            if (@return != null)
            {
                if (canBeInline || returnToVar == null)
                    return @return;

                output.AppendCommand($"set {returnToVar} {@return}");
                return returnToVar;
            }

            string fullName = containingType.ToDisplayString(CompilerHelper.FullNameFormat);
            if (CompilerHelper.IsType<Entity>(fullName)
                || CompilerHelper.IsType<Building>(fullName)
                || CompilerHelper.IsType<Unit>(fullName))
            {
                GenericGet(operation, returnToVar);
                return returnToVar;
            }

            if (CompilerHelper.IsType<LinkedBuilding>(fullName))
            {
                if (operation.Property.Name != nameof(LinkedBuilding.Name))
                    throw CompilerHelper.Error(operation.Syntax, CompilationError.Unknown);
                @return = handler.Handle(operation.Instance, true, output.GetNewTempVar());
                if (@return == null)
                    throw CompilerHelper.Error(operation.Instance.Syntax, CompilationError.NoReturnValue);

                if (canBeInline || returnToVar == null)
                    return $"\"{@return}\"";

                output.AppendCommand($"set {returnToVar} \"{@return}\"");
                return returnToVar;
            }

            if (fullName == typeof(Mindustry).FullName)
                return MindustryProperty(operation, canBeInline, returnToVar);

            throw CompilerHelper.Error(operation.Syntax, CompilationError.UnsupportedOperation, operation);
        }

        private string GetContentType(IPropertySymbol propertySymbol, INamedTypeSymbol containingType)
        {
            var baseType = containingType;
            while (baseType != null)
            {
                if (baseType.IsType<CamelCaseType>())
                {
                    string val = propertySymbol.Name;
                    return $"@{char.ToLower(val[0])}{val.Substring(1)}";
                }

                if (baseType.IsType<KebabCaseType>())
                {
                    string val = propertySymbol.Name;
                    return "@" + val.ToLower().Replace('_', '-');
                }

                baseType = baseType.BaseType;
            }

            return null;
        }

        private string MindustryProperty(IPropertyReferenceOperation operation, bool canBeInline, in string returnToVar)
        {
            string retName = operation.Property.Name switch
            {
                nameof(Mindustry.LinksCount) => "@links",
                nameof(Mindustry.BindingUnit) => "@unit",
                nameof(Mindustry.Ticks) => "@tick",
                nameof(Mindustry.MapWidth) => "@mapw",
                nameof(Mindustry.MapHeight) => "@maph",
                nameof(Mindustry.ItemCount) => "@itemCount",
                nameof(Mindustry.LiquidCount) => "@liquidCount",
                nameof(Mindustry.UnitCount) => "@unitCount",
                nameof(Mindustry.BlockCount) => "@blockCount",
                nameof(Mindustry.BuildingCount) => "@blockCount",
                _ => $"@{operation.Property.Name.ToLower()}",
            };

            if (canBeInline || returnToVar == null)
                return retName;

            output.AppendCommand($"set {returnToVar} {retName}");
            return returnToVar;
        }

        private void GenericGet(IPropertyReferenceOperation operation, in string returnToVar)
        {
            string @return = handler.Handle(operation.Instance, true, output.GetNewTempVar());
            if (@return == null)
                throw CompilerHelper.Error(operation.Instance.Syntax, CompilationError.NoReturnValue);
            string pName = operation.Property.Name;
            output.AppendCommand($"sensor {returnToVar} {@return} @{char.ToLower(pName[0])}{pName.Substring(1)}");
        }
    }
}
