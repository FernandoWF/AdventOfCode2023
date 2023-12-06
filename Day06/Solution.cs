using System.Text.RegularExpressions;

namespace AdventOfCode2023.Day06
{
    internal class Solution : ISolution
    {
        private readonly record struct Race(long Duration, long RecordDistance);

        private static readonly string[] lines = File.ReadAllLines("Day06\\Input.txt");

        public static object RunPart1()
        {
            var races = ParseRaces();
            var numberOfWaysToBeatRecord = races
                .Select(CalculateWaysToBeatRecord)
                .ToList();

            var product = 1;
            foreach (var way in numberOfWaysToBeatRecord)
            {
                product *= way;
            }

            return product;

            static List<Race> ParseRaces()
            {
                var times = Regex.Replace(lines[0], @"\s+", " ")
                    .Split(" ")[1..]
                    .ToList();
                var distances = Regex.Replace(lines[1], @"\s+", " ")
                    .Split(" ")[1..]
                    .ToList();

                return times
                    .Zip(distances)
                    .Select(tuple => new Race(long.Parse(tuple.First), long.Parse(tuple.Second)))
                    .ToList();
            }
        }

        private static int CalculateWaysToBeatRecord(Race race)
        {
            var ways = 0;

            for (var i = 0; i <= race.Duration; i++)
            {
                var holdTime = i;
                var runTime = race.Duration - holdTime;
                var speed = holdTime;
                var distance = speed * runTime;

                if (distance > race.RecordDistance)
                {
                    ways++;
                }
            }

            return ways;
        }

        public static object RunPart2()
        {
            var rawDuration = Regex.Replace(lines[0], @"\s+", string.Empty)
                .Split(":")[1];
            var rawDistance = Regex.Replace(lines[1], @"\s+", string.Empty)
                .Split(":")[1];
            var race = new Race(long.Parse(rawDuration), long.Parse(rawDistance));

            return CalculateWaysToBeatRecord(race);
        }
    }
}
