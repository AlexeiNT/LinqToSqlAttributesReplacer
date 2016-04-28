using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using LinqToSqlAttributesCommon.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesCollector.Implementation
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly AttributesSummary attributesSummary;

        public DocumentProcessor(AttributesSummary attributesSummary)
        {
            this.attributesSummary = attributesSummary;
        }

        public async Task ProcessAsync(Document document)
        {
            var documentSyntax = await document.GetSyntaxRootAsync().ConfigureAwait(false);
            var entityDeclaration = documentSyntax.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(x => x.HasAttribute(AttributeName.Table))
                .ToArray();

            var classAttributes = entityDeclaration.SelectMany(x => x.SelectAttributes(AttributeName.Table)).ToArray();
            var classAttributesStrings = classAttributes.Select(x => x.Name.ToString()).ToArray();
            var classParamters = classAttributes.SelectMany(p => p.SelectParameters()).Select(p => p.NameEquals.Name.ToString()).ToArray();


            var propertyAttributesList = new List<string>();
            var propertyAttributeArgumentsList = new List<string>();


            foreach (var classDeclaration in entityDeclaration)
            {
                var entityProperties = classDeclaration.DescendantNodes()
                    .OfType<PropertyDeclarationSyntax>()
                    .Where(x => x.HasAttribute(AttributeName.Column))
                    .ToArray();

                var propertyAttributes = entityProperties.SelectMany(x => x.SelectAttributes(AttributeName.Column)).ToArray();
                var propertyAttributesStrings = propertyAttributes.Select(x => x.Name.ToString());

                var propertyParamters = propertyAttributes.SelectMany(p => p.SelectParameters()).Select(p => p.NameEquals.Name.ToString());
                propertyAttributesList.AddRange(propertyAttributesStrings);
                propertyAttributeArgumentsList.AddRange(propertyParamters);
            }

            attributesSummary.AddClassAttributeNames(classAttributesStrings);
            attributesSummary.AddClassAttributeParamtersNames(classParamters);
            attributesSummary.AddPropertyAttributeNames(propertyAttributesList.ToArray());
            attributesSummary.AddPropertyAttributeParametersNames(propertyAttributeArgumentsList.ToArray());
        }
    }
}