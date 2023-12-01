namespace AdventOfCode2023.Day01
{
    internal class Solution : ISolution
    {
        public static object RunPart1()
        {
            var lines = File.ReadAllLines("Day01\\Input.txt");
            var sum = 0;
            foreach (var line in lines)
            {
                var digits = line.Where(char.IsDigit);
                var number = int.Parse($"{digits.First()}{digits.Last()}");
                sum += number;
            }

            return sum;
        }

        public static object? RunPart2()
        {
            return null;
        }
    }
}
