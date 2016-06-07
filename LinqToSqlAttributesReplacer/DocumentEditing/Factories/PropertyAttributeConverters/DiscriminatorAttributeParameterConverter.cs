using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public class DiscriminatorAttributeParameterConverter : IAttributeParameterConverter
    {
        private const string columnIsDescriminator = AttributeArgumentName.ColumnIsDescriminator;

        public int Priority => 100500;

        public AttributeSyntax Convert(AttributeSyntax attribute)
        {
            return null;
        }
    }
}