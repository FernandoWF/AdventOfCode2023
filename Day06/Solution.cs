using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day06
{
    internal class Solution : ISolution
    {
        private readonly record struct Race(int Duration, int RecordDistance);

        private static readonly string[] lines = File.ReadAllLines("Day06\\Input.txt");

        public static object RunPart1()
        {
            var races = ParseRaces();
            var numberOfWaysToBeatRecord = races
                .Select(r =>
                {
                    var ways = 0;

                    for (var i = 0; i <= r.Duration; i++)
                    {
                        var holdTime = i;
                        var runTime = r.Duration - holdTime;
                        var speed = holdTime;
                        var distance = speed * runTime;

                        if (distance > r.RecordDistance)
                        {
                            ways++;
                        }
                    }

                    return ways;
                })
                .ToList();

            var product = 1;
            foreach (var way in numberOfWaysToBeatRecord)
            {
                product *= way;
            }

            return product;
        }

        private static List<Race> ParseRaces()
        {
            var times = Regex.Replace(lines[0], @"\s+", " ")
                .Split(" ")[1..]
                .ToList();
            var distances = Regex.Replace(lines[1], @"\s+", " ")
                .Split(" ")[1..]
                .ToList();

            return times
                .Zip(distances)
                .Select(tuple => new Race(int.Parse(tuple.First), int.Parse(tuple.Second)))
                .ToList();
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
