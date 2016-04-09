using System;

namespace LinqToSqlAttributesCommon.Helpers
{
    public static class ConsoleHelper
    {
        private static readonly object consoleLocker = new object();

        public static void Write(string message)
        {
            lock (consoleLocker)
            {
                Console.WriteLine(message);
            }
        }

        public static string ChooseSingle(string[] solutions)
        {
            lock (consoleLocker)
            {
                for (var i = 0; i < solutions.Length; i++)
                {
                    Console.WriteLine($"{i} : {solutions[i]}");
                }

                while (true)
                {
                    Console.WriteLine("Please enter actual solution index");
                    var actualSolutionIndex = 0;
                    if (int.TryParse(Console.ReadLine(), out actualSolutionIndex) && actualSolutionIndex >= 0 &&
                        actualSolutionIndex <= solutions.Length)
                    {
                        return solutions[actualSolutionIndex];
                    }
                    Console.WriteLine("Wrong input, try again");
                }
            }
        }

        public static void Pause()
        {
            lock (consoleLocker)
            {
                Console.WriteLine("Press any key for continue...");
                Console.ReadKey();
            }
        }
    }
}
