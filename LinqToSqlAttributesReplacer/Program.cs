using System;
using System.Diagnostics;
using LightInject;
using LinqToSqlAttributesCommon;
using LinqToSqlAttributesCommon.Helpers;

namespace LinqToSqlAttributesReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var container = CreateContainerInstance();
                
                var solutionPath = SolutionHelper.GetSolutionPath(args);
                var solutionProcessor = container.GetInstance<ISolutionProcessor>();
                solutionProcessor.ProcessAsync(solutionPath).Wait();
            }
            catch (Exception e)
            {
                ConsoleHelper.Write($"{e.Message}\n{e.StackTrace}");
            }
            finally
            {
                ConsoleHelper.Write($"Finished in {stopwatch.ElapsedMilliseconds} ms.");
                ConsoleHelper.Pause();
            }
        }

        private static ServiceContainer CreateContainerInstance()
        {
            var container = new ServiceContainer();
            var containerConfigurator = new ContainerConfigurator();
            containerConfigurator.Configure(container);

            return container;
        }
    }
}
