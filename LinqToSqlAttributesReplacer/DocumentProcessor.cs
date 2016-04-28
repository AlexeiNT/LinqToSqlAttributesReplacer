using System.Linq;
using System.Threading.Tasks;
using LinqToSqlAttributesCommon.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace LinqToSqlAttributesReplacer
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IDocumentEditingCommandsFactory documentEditingCommandsFactory;
        private readonly IDocumentWriter documentWriter;

        public DocumentProcessor(
            IDocumentEditingCommandsFactory documentEditingCommandsFactory,
            IDocumentWriter documentWriter)
        {
            this.documentEditingCommandsFactory = documentEditingCommandsFactory;
            this.documentWriter = documentWriter;
        }

        public async Task ProcessAsync(Document document)
        {
            var documentSyntax = await document.GetSyntaxRootAsync().ConfigureAwait(false);
            var documentEditingCommands = documentEditingCommandsFactory.Create(documentSyntax);

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