using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace LinqToSqlAttributesCommon.Interfaces
{
    public interface IDocumentProcessor
    {
        Task ProcessAsync(Document document);
    }
}