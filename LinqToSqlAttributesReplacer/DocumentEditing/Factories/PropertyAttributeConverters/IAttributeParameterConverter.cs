using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters
{
    public interface IAttributeParameterConverter
    {
        AttributeSyntax Convert(AttributeSyntax attribute);
        int Priority { get; }
    }
}