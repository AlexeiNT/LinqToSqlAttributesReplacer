using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public interface IAttributeParameterConverter
    {
        AttributeSyntax Convert(AttributeSyntax attribute);
    }

    //TODO: Implement (Right)
    public class UpdateCheckAttributeParameterConverter : IAttributeParameterConverter
    {
        public AttributeSyntax Convert(AttributeSyntax attribute)
        {

            //TODO: Didn't work, its Enum value
            var canBeNull = attribute
                .FindArgument(AttributeArgumentName.ColumnIsCheckUpdate)
                ?.GetBoolValue();

            return canBeNull == false
                ? CreateReplacement()
                : null;
        }

        private static AttributeSyntax CreateReplacement()
        {
            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.ConcurrencyCheck));
        }
    }

    public class DiscriminatorAttributeParameterConverter : IAttributeParameterConverter
    {
        private static string columnIsDescriminator = AttributeArgumentName.ColumnIsDescriminator;

        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            throw new System.NotImplementedException();
        }
    }
}