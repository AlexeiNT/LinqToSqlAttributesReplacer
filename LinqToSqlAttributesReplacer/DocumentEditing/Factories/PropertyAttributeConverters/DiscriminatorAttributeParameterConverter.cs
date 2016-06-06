using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    //TODO: Implement (Right)
    public class DiscriminatorAttributeParameterConverter : IAttributeParameterConverter
    {
        private const string columnIsDescriminator = AttributeArgumentName.ColumnIsDescriminator;

        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            throw new NotImplementedException();
        }
    }
}