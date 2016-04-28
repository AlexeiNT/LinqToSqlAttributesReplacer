using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer
{
    public interface IDocumentWriter
    {
        void Write(Document updatedDocument);
    }
}