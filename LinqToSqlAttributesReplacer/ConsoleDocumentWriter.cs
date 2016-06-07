using System.Threading.Tasks;
using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer
{
    public class ConsoleDocumentWriter : IDocumentWriter
    {
        public async Task WriteAsync(Document updatedDocument)
        {
            var documentSource = await updatedDocument.GetSyntaxRootAsync().ConfigureAwait(false);
            ConsoleHelper.Write(documentSource.NormalizeWhitespace().ToString());
        }
    }
}