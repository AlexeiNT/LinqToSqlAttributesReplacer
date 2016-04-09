using System;
using System.Diagnostics;
using System.IO;
using LinqToSqlAttributesCollector.Implementation;
using LinqToSqlAttributesCommon;
using LinqToSqlAttributesCommon.Extensions;
using LinqToSqlAttributesCommon.Helpers;

namespace LinqToSqlAttributesCollector
{
    class Programm
    {
        static void Main(string[] args)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var solutionPath = SolutionHelper.GetSolutionPath(args);
                var attributesSummary = new AttributesSummary();
                var solutionProcessor = new SolutionProcessor(new DocumentProcessor(attributesSummary));

                solutionProcessor.ProcessAsync(solutionPath).Wait();

                ConsoleHelper.Write(attributesSummary.ToString());
                File.WriteAllLines( @"E:\atributes.txt", new [] {attributesSummary.ToString()} );
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
    }
}
