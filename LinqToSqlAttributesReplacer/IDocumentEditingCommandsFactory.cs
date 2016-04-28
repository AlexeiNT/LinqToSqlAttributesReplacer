using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer
{
    public interface IDocumentEditingCommandsFactory
    {
        IDocumentEditingCommands[] Create(SyntaxNode documentSyntax);
    }
}