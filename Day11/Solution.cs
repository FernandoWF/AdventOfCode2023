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
            var universe = ConvertTo2dArray(jaggedUniverse);
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

        public static T[,] ConvertTo2dArray<T>(T[][] jaggedArray)
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
            (var emptyRowIndexes, var emptyColumnIndexes) = GetEmptyIndexes(universe);
            var rowCount = universe.GetLength(1);
            var columnCount = universe.GetLength(0);
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

        private static (HashSet<int> emptyRowIndexes, HashSet<int> emptyColumnIndexes) GetEmptyIndexes(char[,] universe)
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

            return (emptyRowIndexes, emptyColumnIndexes);
        }

        public static object RunPart2()
        {
            const int ExpandedRowOrColumnSize = 1000000;
            const int ExpansionSize = ExpandedRowOrColumnSize - 1;
            var jaggedUniverse = lines
                .Select(l => l.ToCharArray())
                .ToArray();
            var universe = ConvertTo2dArray(jaggedUniverse);
            (var emptyRowIndexes, var emptyColumnIndexes) = GetEmptyIndexes(universe);

            var galaxies = new List<(int X, int Y)>();
            var columnCount = universe.GetLength(0);
            var rowCount = universe.GetLength(1);

            for (var y = 0; y < rowCount; y++)
            {
                for (var x = 0; x < columnCount; x++)
                {
                    if (universe[x, y] == '#')
                    {
                        galaxies.Add((x, y));
                    }
                }
            }

            var shortestPathLengthSum = 0L;
            for (var galaxy1Index = 0; galaxy1Index < galaxies.Count; galaxy1Index++)
            {
                for (var galaxy2Index = galaxy1Index + 1; galaxy2Index < galaxies.Count; galaxy2Index++)
                {
                    var (galaxy1X, galaxy1Y) = galaxies[galaxy1Index];
                    var (galaxy2X, galaxy2Y) = galaxies[galaxy2Index];

                    var minX = Math.Min(galaxy1X, galaxy2X);
                    var maxX = Math.Max(galaxy1X, galaxy2X);
                    var minY = Math.Min(galaxy1Y, galaxy2Y);
                    var maxY = Math.Max(galaxy1Y, galaxy2Y);

                    var shortestPathHorizontalDistance = maxX - minX;
                    for (var x = minX + 1; x < maxX; x++)
                    {
                        if (emptyColumnIndexes.Contains(x))
                        {
                            shortestPathHorizontalDistance += ExpansionSize;
                        }
                    }

                    var shortestPathVerticalDistance = maxY - minY;
                    for (var y = minY + 1; y < maxY; y++)
                    {
                        if (emptyRowIndexes.Contains(y))
                        {
                            shortestPathVerticalDistance += ExpansionSize;
                        }
                    }

                    shortestPathLengthSum += shortestPathHorizontalDistance + shortestPathVerticalDistance;
                }
            }

            return shortestPathLengthSum;
        }
    }
}
