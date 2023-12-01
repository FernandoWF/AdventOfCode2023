namespace AdventOfCode2023
{
    internal static class DayRunner<TDay> where TDay : ISolution
    {

        public static void Run()
        {
            Console.WriteLine("========== Part 1 ==========");
            var part1Solution = TDay.RunPart1();
            if (part1Solution is not null)
            {
                Console.WriteLine(part1Solution.ToString());
            }

            Console.WriteLine();

            Console.WriteLine("========== Part 2 ==========");
            var part2Solution = TDay.RunPart2();
            if (part2Solution is not null)
            {
                Console.WriteLine(part2Solution.ToString());
            }
        }
    }
}
