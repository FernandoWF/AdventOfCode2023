namespace AdventOfCode2023.Day11
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day11\\Input.txt");

        public static object RunPart1()
        {
            var jaggedUniverse = lines
                .Select(l => l.ToCharArray())
                .ToArray();
            var universe = To2dArray(jaggedUniverse);
            var expandedUniverse = ExpandUniverse(universe);

            var galaxies = new List<(int X, int Y)>();
            var columnCount = expandedUniverse.GetLength(0);
            var rowCount = expandedUniverse.GetLength(1);

            for (var y = 0; y < rowCount; y++)
            {
                for (var x = 0; x < columnCount; x++)
                {
                    if (expandedUniverse[x, y] == '#')
                    {
                        galaxies.Add((x, y));
                    }
                }
            }

            var shortestPathLengthSum = 0;
            for (var galaxy1Index = 0; galaxy1Index < galaxies.Count; galaxy1Index++)
            {
                for (var galaxy2Index = galaxy1Index + 1; galaxy2Index < galaxies.Count; galaxy2Index++)
                {
                    var (galaxy1X, galaxy1Y) = galaxies[galaxy1Index];
                    var (galaxy2X, galaxy2Y) = galaxies[galaxy2Index];

                    var shortestPathDistance = Math.Abs(galaxy1X - galaxy2X) + Math.Abs(galaxy1Y - galaxy2Y);
                    shortestPathLengthSum += shortestPathDistance;
                }
            }

            return shortestPathLengthSum;
        }

        public static T[,] To2dArray<T>(T[][] jaggedArray)
        {
            var firstInnerArrayLength = jaggedArray[0].Length;
            var array = new T[firstInnerArrayLength, jaggedArray.Length];

            for (var y = 0; y < jaggedArray.Length; y++)
            {
                var innerArray = jaggedArray[y];
                if (innerArray.Length != firstInnerArrayLength)
                {
                    throw new ArgumentException("Not all rows of array have the same size.");
                }

                for (var x = 0; x < innerArray.Length; x++)
                {
                    array[x, y] = innerArray[x];
                }
            }

            return array;
        }

        private static char[,] ExpandUniverse(char[,] universe)
        {
            var emptyRowIndexes = new HashSet<int>();
            var rowCount = universe.GetLength(1);

            var emptyColumnIndexes = new HashSet<int>();
            var columnCount = universe.GetLength(0);

            for (var y = 0; y < rowCount; y++)
            {
                var emptyRow = true;
                for (var x = 0; x < columnCount && emptyRow; x++)
                {
                    if (universe[x, y] == '#')
                    {
                        emptyRow = false;
                    }
                }

                if (emptyRow)
                {
                    emptyRowIndexes.Add(y);
                }
            }

            for (var x = 0; x < columnCount; x++)
            {
                var emptyColumn = true;
                for (var y = 0; y < rowCount && emptyColumn; y++)
                {
                    if (universe[x, y] == '#')
                    {
                        emptyColumn = false;
                    }
                }

                if (emptyColumn)
                {
                    emptyColumnIndexes.Add(x);
                }
            }

            var newColumnCount = columnCount + emptyColumnIndexes.Count;
            var newRowCount = rowCount + emptyRowIndexes.Count;
            var resizedUniverse = new char[newColumnCount, newRowCount];

            for (int y = 0, originalUniverseY = 0; y < newRowCount; y++, originalUniverseY++)
            {
                if (emptyRowIndexes.Contains(originalUniverseY))
                {
                    for (var x = 0; x < newColumnCount; x++)
                    {
                        resizedUniverse[x, y] = '.';
                        resizedUniverse[x, y + 1] = '.';
                    }
                    y++;
                }
                else
                {
                    for (int x = 0, originalUniverseX = 0; x < newColumnCount; x++, originalUniverseX++)
                    {
                        if (emptyColumnIndexes.Contains(originalUniverseX))
                        {
                            resizedUniverse[x, y] = '.';
                            resizedUniverse[x + 1, y] = '.';
                            x++;
                        }
                        else
                        {
                            resizedUniverse[x, y] = universe[originalUniverseX, originalUniverseY];
                        }
                    }
                }
            }

            return resizedUniverse;
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
