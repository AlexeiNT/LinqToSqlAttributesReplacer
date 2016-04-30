using System.Linq;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;
using LinqToSqlAttributesReplacer.DocumentEditing.Commands;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories
{
    public class TableAttributesReplacementCommandFactory : IDocumentEditingConcreteFactory
    {
        public bool CanCreate(SyntaxNode documentSyntax)
        {
            return documentSyntax.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Any(x => x.HasAttribute(AttributeName.Table));
        }

        public IDocumentEditingCommand[] Create(SyntaxNode documentSyntax)
        {
            var actualClassDeclarations = documentSyntax
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(x => x.HasAttribute(AttributeName.Table))
                .ToArray();

            var attributesForReplacement = actualClassDeclarations.SelectMany(x => x.SelectAttributes(AttributeName.Table));
            return attributesForReplacement.Select(x => CreateCommand(x, CreateReplacement(x))).ToArray();
        }

        private static AttributeSyntax CreateReplacement(AttributeSyntax attributeSyntax)
        {
            var tableName = attributeSyntax.GetArgument(AttributeArgumentName.TableName).GetStringValue();

            var attributeArgumentListSyntax = SyntaxFactory.AttributeArgumentList(
                SyntaxFactory.SeparatedList(new[]
                {
                    SyntaxFactory.AttributeArgument(
                        SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                            SyntaxFactory.Literal(tableName)))
                }));

            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName.Table), attributeArgumentListSyntax);
        }

        private static IDocumentEditingCommand CreateCommand(AttributeSyntax oldAttribute, AttributeSyntax newAttribute)
        {
            return new SyntaxReplacementCommand(oldAttribute, newAttribute);
        }
    }
}