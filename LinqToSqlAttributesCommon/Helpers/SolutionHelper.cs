using System;
using System.IO;

namespace LinqToSqlAttributesCommon.Helpers
{
    public static class SolutionHelper
    {
        public static string GetSolutionPath(string[] args)
        {
            var solutionDirectory = args.Length == 0
                    ? Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                    : args[0];

            var solutions = Directory.GetFiles(solutionDirectory, "*.sln");
            if (solutions.Length == 0)
            {
                throw new FileNotFoundException("There is no any *.sln file");
            }

            return solutions.Length > 1
                ? ConsoleHelper.ChooseSingle(solutions)
                : solutions[0];
        }
    }
}