using System.Linq;
using System.Threading.Tasks;
using LinqToSqlAttributesCommon.Interfaces;
using LinqToSqlAttributesReplacer.DocumentEditing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IDocumentEditingCommandsCompositeFactory documentEditingCommandsCompositeFactory;
        private readonly IDocumentWriter documentWriter;

        public DocumentProcessor(
            IDocumentEditingCommandsCompositeFactory documentEditingCommandsCompositeFactory,
            IDocumentWriter documentWriter)
        {
            this.documentEditingCommandsCompositeFactory = documentEditingCommandsCompositeFactory;
            this.documentWriter = documentWriter;
        }

        public async Task ProcessAsync(Document document)
        {
            var documentSyntax = await document.GetSyntaxRootAsync().ConfigureAwait(false);
            var documentEditingCommands = documentEditingCommandsCompositeFactory.Create(documentSyntax);

            if (documentEditingCommands.Length == 0)
            {
                return;
            }

            var documentEditor = await DocumentEditor.CreateAsync(document).ConfigureAwait(false);
            documentEditingCommands.Aggregate(documentEditor, (editor, command) => command.Execute(documentEditor));
            var updatedDocument = documentEditor.GetChangedDocument();

            documentWriter.Write(updatedDocument);
        }
    }
}