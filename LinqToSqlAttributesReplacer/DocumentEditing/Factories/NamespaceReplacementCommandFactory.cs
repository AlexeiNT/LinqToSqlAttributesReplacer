using System.Linq;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesReplacer.DocumentEditing.Commands;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Factories
{
    public class NamespaceReplacementCommandFactory : IDocumentEditingConcreteFactory
    {
        public bool CanCreate(SyntaxNode documentSyntax)
        {
            return documentSyntax.SelectUsingDirectives(NamespaceName.LinqDataMappng).Any();
        }

        public IDocumentEditingCommand[] Create(SyntaxNode documentSyntax)
        {
            var usingDirective = documentSyntax.SelectUsingDirectives(NamespaceName.LinqDataMappng).First();
            return new[] {CreateCommand(usingDirective, CreateReplacement())};
        }

        private static UsingDirectiveSyntax[] CreateReplacement()
        {
            var dataUsing = CreateUsing(NamespaceName.EntityFrameworkData);
            var schemaUsing = CreateUsing(NamespaceName.EntityFrameworkSchema);
            return new[] {dataUsing, schemaUsing};
        }

        private static UsingDirectiveSyntax CreateUsing(string usingPath)
        {
            return SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName(usingPath));
        }

        private static IDocumentEditingCommand CreateCommand(UsingDirectiveSyntax old, UsingDirectiveSyntax[] @new)
        {
            return new UsingReplacementCommand(old, @new);
        }
    }
}