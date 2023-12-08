using System.Diagnostics;

namespace AdventOfCode2023
{
    internal static class DayRunner<TDay> where TDay : ISolution
    {
        public static void Run()
        {
            var stopwatch = new Stopwatch();
            Console.WriteLine("========== Part 1 ==========");

            stopwatch.Start();
            var part1Solution = TDay.RunPart1();
            stopwatch.Stop();

            Console.WriteLine(part1Solution?.ToString());
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");

            Console.WriteLine();
            Console.WriteLine("========== Part 2 ==========");

            stopwatch.Restart();
            var part2Solution = TDay.RunPart2();
            stopwatch.Stop();

            Console.WriteLine(part2Solution?.ToString());
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
        }
    }
}
