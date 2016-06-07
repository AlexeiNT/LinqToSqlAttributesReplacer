using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public class UpdateCheckAttributeParameterConverter : IAttributeParameterConverter
    {
        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            var canBeNull = attribute
                .FindArgument(AttributeArgumentName.ColumnIsCheckUpdate)
                ?.GetEnumValue();

            return string.IsNullOrEmpty(canBeNull) || canBeNull == "UpdateCheck.Never"
                ? null
                : CreateReplacement();
        }

        private static AttributeSyntax CreateReplacement()
        {
            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.EntityConcurrencyCheck));
        }
    }
}