using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public class PrimaryKeyAttributeParameterConverter : IAttributeParameterConverter
    {
        public int Priority => 10;

        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            var isPrimary = attribute
                .FindArgument(AttributeArgumentName.ColumnIsPrimary)
                ?.GetBoolValue();

            return isPrimary == true
                ? CreateReplacement()
                : null;
        }

        private static AttributeSyntax CreateReplacement()
        {
            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.EntityKey));
        }
    }
}