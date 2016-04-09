using System.Linq;
using System.Threading.Tasks;
using LinqToSqlAttributesCommon.Helpers;
using LinqToSqlAttributesCommon.Interfaces;
using Microsoft.CodeAnalysis.MSBuild;

namespace LinqToSqlAttributesCommon
{
    public class SolutionProcessor
    {
        private readonly IDocumentProcessor documentProcessor;

        public SolutionProcessor(IDocumentProcessor documentProcessor)
        {
            this.documentProcessor = documentProcessor;
        }

        public async Task ProcessAsync(string solutionPath)
        {
            var msBuildWorkspace = MSBuildWorkspace.Create();
            var solution = await msBuildWorkspace.OpenSolutionAsync(solutionPath).ConfigureAwait(false);
            
            var i = 0;
            var projectsCount = solution.Projects.Count();
            foreach (var project in solution.Projects)
            {
                var documentsProcessingTask = project.Documents.Select(x => documentProcessor.ProcessAsync(x));
                await Task.WhenAll(documentsProcessingTask);

                ConsoleHelper.Write($"{++i}/{projectsCount} : {project.Name} was processed");
            }
            msBuildWorkspace.CloseSolution();
        }
    }
}