using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer.DocumentEditing
{
    public interface IDocumentEditingConcreteFactory
    {
        bool CanCreate(SyntaxNode documentSyntax);
        IDocumentEditingCommand[] Create(SyntaxNode documentSyntax);
    }
}