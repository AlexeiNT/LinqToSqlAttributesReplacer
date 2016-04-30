using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer.DocumentEditing
{
    public interface IDocumentEditingCommandsCompositeFactory
    {
        IDocumentEditingCommand[] Create(SyntaxNode documentSyntax);
    }
}