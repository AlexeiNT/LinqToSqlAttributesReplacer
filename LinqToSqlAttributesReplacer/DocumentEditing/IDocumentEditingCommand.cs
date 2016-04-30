using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer.DocumentEditing
{
    public interface IDocumentEditingCommand
    {
        DocumentEditor Execute(DocumentEditor documentEditor);
    }
}