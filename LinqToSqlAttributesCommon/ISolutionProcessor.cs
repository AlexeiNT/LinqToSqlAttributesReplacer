using System.Threading.Tasks;

namespace LinqToSqlAttributesCommon
{
    public interface ISolutionProcessor
    {
        Task ProcessAsync(string solutionPath);
    }
}