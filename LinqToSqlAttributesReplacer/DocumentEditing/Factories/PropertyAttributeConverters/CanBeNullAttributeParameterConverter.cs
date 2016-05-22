using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public class CanBeNullAttributeParameterConverter : IAttributeParameterConverter
    {
        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            var canBeNull = attribute
                .FindArgument(AttributeArgumentName.ColumnIsNullable)
                ?.GetBoolValue();

            return canBeNull == false
                ? CreateReplacement()
                : null;
        }

        private static AttributeSyntax CreateReplacement()
        {
            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.EntityRequired));
        }
    }
}