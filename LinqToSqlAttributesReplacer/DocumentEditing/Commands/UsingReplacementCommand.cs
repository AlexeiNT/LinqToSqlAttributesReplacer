using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Commands
{
    public class UsingReplacementCommand : IDocumentEditingCommand
    {
        private readonly UsingDirectiveSyntax forReplacement;
        private readonly UsingDirectiveSyntax[] newUsings;

        public UsingReplacementCommand(UsingDirectiveSyntax forReplacement, UsingDirectiveSyntax[] newUsings)
        {
            this.forReplacement = forReplacement;
            this.newUsings = newUsings;
        }

        public DocumentEditor Execute(DocumentEditor documentEditor)
        {
            foreach (var newUsing in newUsings)
            {
                documentEditor.InsertAfter(forReplacement, newUsing);
            }

            documentEditor.RemoveNode(forReplacement);
            return documentEditor;
        }
    }
}