namespace AdventOfCode2023.Day14
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day14\\Input.txt");

        public static object RunPart1()
        {
            var height = lines.Length;
            var width = lines[0].Length;
            var grid = new char[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    grid[x, y] = lines[y][x];
                }
            }

            return TiltNorth();

            int TiltNorth()
            {
                var totalLoad = 0;

                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var character = grid[x, y];
                        if (character == 'O')
                        {
                            var newY = MoveRoundedRockNorth(x, y);
                            totalLoad += height - newY;
                        }
                    }
                }

                return totalLoad;

                int MoveRoundedRockNorth(int x, int y)
                {
                    if (y == 0)
                    {
                        return 0;
                    }

                    var notEmptyY = y - 1;

                    while (notEmptyY >= 0 && grid![x, notEmptyY] == '.')
                    {
                        notEmptyY--;
                    }

                    var newY = notEmptyY + 1;
                    if (newY < y)
                    {
                        grid![x, newY] = 'O';
                        grid![x, y] = '.';
                    }

                    return newY;
                }
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
