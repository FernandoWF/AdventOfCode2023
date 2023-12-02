namespace AdventOfCode2023.Day02
{
    internal class Solution : ISolution
    {
        internal record struct Game(int Id, int MostRed, int MostGreen, int MostBlue);

        private static readonly string[] lines = File.ReadAllLines("Day02\\Input.txt");
        private const int TotalRed = 12;
        private const int TotalGreen = 13;
        private const int TotalBlue = 14;

        public static object RunPart1()
        {
            var games = lines.Select((line, index) =>
            {
                var startingCharacterIndex = line.IndexOf(':') + 2;
                var red = 0;
                var green = 0;
                var blue = 0;
                var sets = line[startingCharacterIndex..].Split(';');
                foreach (var set in sets)
                {
                    var setRed = FindQuantity(set, "red");
                    if (setRed > red)
                    {
                        red = setRed;
                    }

                    var setGreen = FindQuantity(set, "green");
                    if (setGreen > green)
                    {
                        green = setGreen;
                    }

                    var setBlue = FindQuantity(set, "blue");
                    if (setBlue > blue)
                    {
                        blue = setBlue;
                    }
                }

                return new Game(index + 1, red, green, blue);
            }).ToList();

            var possibleGames = games.Where(g => g.MostRed <= TotalRed && g.MostGreen <= TotalGreen && g.MostBlue <= TotalBlue);

            return possibleGames.Sum(g => g.Id);

            static int FindQuantity(string set, string color)
            {
                var colorIndex = set.IndexOf(color);

                if (colorIndex == -1)
                {
                    return 0;
                }

                var trailingSpaceIndex = colorIndex - 1;
                var leadingSpaceIndex = set[..(trailingSpaceIndex - 1)].LastIndexOf(' ');
                if (leadingSpaceIndex == -1)
                {
                    leadingSpaceIndex = 0;
                }

                return int.Parse(set[leadingSpaceIndex..trailingSpaceIndex]);
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
