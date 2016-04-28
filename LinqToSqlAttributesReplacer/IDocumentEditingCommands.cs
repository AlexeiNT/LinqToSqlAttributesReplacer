using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer
{
    public interface IDocumentEditingCommands
    {
        DocumentEditor Execute(DocumentEditor documentEditor);
    }
}