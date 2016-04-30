using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer.DocumentEditing.Commands
{
    public class SyntaxReplacementCommand : IDocumentEditingCommand
    {
        private readonly SyntaxNode nodeForReplacement;
        private readonly SyntaxNode newNode;

        public SyntaxReplacementCommand(SyntaxNode nodeForReplacement, SyntaxNode newNode)
        {
            this.nodeForReplacement = nodeForReplacement;
            this.newNode = newNode;
        }

        public DocumentEditor Execute(DocumentEditor documentEditor)
        {
            documentEditor.ReplaceNode(nodeForReplacement, newNode);
            return documentEditor;
        }
    }
}