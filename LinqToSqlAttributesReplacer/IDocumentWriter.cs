using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesReplacer
{
    public interface IDocumentWriter
    {
        Task WriteAsync(Document updatedDocument);
    }
}