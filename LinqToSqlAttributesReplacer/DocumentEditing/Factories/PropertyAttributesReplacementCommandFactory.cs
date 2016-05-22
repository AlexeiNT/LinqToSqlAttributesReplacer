using System.Collections.Generic;
using System.Linq;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using LinqToSqlAttributesReplacer.DocumentEditing.Commands;
using LinqToSqlAttributesReplacer.DocumentEditing.Factories.PropertyAttributeConverters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories
{
    public class PropertyAttributesReplacementCommandFactory : IDocumentEditingConcreteFactory
    {
        private readonly IAttributeParameterConverter[] attributeParameterConverters;

        public PropertyAttributesReplacementCommandFactory(IAttributeParameterConverter[] attributeParameterConverters)
        {
            this.attributeParameterConverters = attributeParameterConverters;
        }

        public bool CanCreate(SyntaxNode documentSyntax)
        {
            return documentSyntax.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Any(x => x.HasAttribute(AttributeName.Column));
        }

        public IDocumentEditingCommand[] Create(SyntaxNode documentSyntax)
        {
            var actualClassDeclarations = documentSyntax
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(x => x.HasAttribute(AttributeName.Column))
                .ToArray();

            var attributesForReplacement = actualClassDeclarations.SelectMany(x => x.SelectAttributes(AttributeName.Column)).ToArray();
            return attributesForReplacement.Select(Convert).ToArray();
        }

        private IDocumentEditingCommand Convert(AttributeSyntax attribute)
        {
            var result = attributeParameterConverters
                .Select(x => x.Convert(attribute))
                .Where(x => x != null)
                .ToArray();

            var replacement = SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(result));
            return CreateCommand(attribute, replacement);
        }

        private static IDocumentEditingCommand CreateCommand(AttributeSyntax oldAttribute, AttributeListSyntax newAttribute)
        {
            return new SyntaxReplacementCommand(oldAttribute, newAttribute);
        }
    }
}