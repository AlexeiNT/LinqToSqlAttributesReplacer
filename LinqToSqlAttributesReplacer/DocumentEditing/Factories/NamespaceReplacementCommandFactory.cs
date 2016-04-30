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
            return documentSyntax
                .SelectUsingDirectives(NamespaceName.LinqDataMappng)
                .Select(x => CreateCommand(x, CreateReplacement(x)))
                .ToArray();
        }

        private static UsingDirectiveSyntax CreateReplacement(UsingDirectiveSyntax usingDirective)
        {
            return usingDirective.WithName(SyntaxFactory.IdentifierName(NamespaceName.EntityFrameworkDataMapping));
        }

        private static IDocumentEditingCommand CreateCommand(UsingDirectiveSyntax old, UsingDirectiveSyntax @new)
        {
            return new SyntaxReplacementCommand(old, @new);
        }
    }
}