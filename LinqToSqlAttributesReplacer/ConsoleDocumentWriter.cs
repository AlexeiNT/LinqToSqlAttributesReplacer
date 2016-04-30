using LinqToSqlAttributesCommon.Helpers;
using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer
{
    public class ConsoleDocumentWriter : IDocumentWriter
    {
        public void Write(Document updatedDocument)
        {
            var documentSource = updatedDocument.GetSyntaxRootAsync().Result.NormalizeWhitespace().ToString();
            ConsoleHelper.Write(documentSource);
        }
    }
}