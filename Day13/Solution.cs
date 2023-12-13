using System.Diagnostics;

namespace AdventOfCode2023.Day13
{
    internal class Solution : ISolution
    {
        private static readonly string text = File.ReadAllText("Day13\\Input.txt");

        public static object RunPart1()
        {
            var patterns = text.Split($"{Environment.NewLine}{Environment.NewLine}");
            var patternLines = patterns
                .Select(p => p.Trim().Split($"{Environment.NewLine}"))
                .ToList();
            var patternGrids = patternLines
                .Select(p =>
                {
                    var height = p.Length;
                    var width = p[0].Length;
                    var grid = new char[width, height];

                    for (var y = 0; y < height; y++)
                    {
                        for (var x = 0; x < width; x++)
                        {
                            grid[x, y] = p[y][x];
                        }
                    }

                    return grid;
                })
                .ToList();

            return patternGrids
                .Select(GetPatternNoteSummary)
                .Sum();

            static int GetPatternNoteSummary(char[,] pattern)
            {
                var width = pattern.GetLength(0);
                var height = pattern.GetLength(1);

                var repeatedColumnIndexes = GetRepeatedColumnIndexes();
                foreach (var index in repeatedColumnIndexes)
                {
                    if (IsHorizontalMirror(index))
                    {
                        return index + 1;
                    }
                }

                var repeatedRowIndexes = GetRepeatedRowIndexes();
                foreach (var index in repeatedRowIndexes)
                {
                    if (IsVerticalMirror(index))
                    {
                        return (index + 1) * 100;
                    }
                }

                throw new UnreachableException("There should be a reflection in the pattern.");

                IEnumerable<int> GetRepeatedColumnIndexes()
                {
                    for (var x = 0; x < width - 1; x++)
                    {
                        if (AreColumnsEqual(x, x + 1))
                        {
                            yield return x;
                        }
                    }
                }

                bool IsHorizontalMirror(int firstRepeatedColumn)
                {
                    for (var x = 0; firstRepeatedColumn + x + 1 < width && firstRepeatedColumn - x >= 0; x++)
                    {
                        if (!AreColumnsEqual(firstRepeatedColumn - x, firstRepeatedColumn + x + 1))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                bool AreColumnsEqual(int x1, int x2)
                {
                    for (var y = 0; y < height; y++)
                    {
                        if (pattern[x1, y] != pattern[x2, y])
                        {
                            return false;
                        }
                    }

                    return true;
                }

                IEnumerable<int> GetRepeatedRowIndexes()
                {
                    for (var y = 0; y < height - 1; y++)
                    {
                        if (AreRowsEqual(y, y + 1))
                        {
                            yield return y;
                        }
                    }
                }

                bool IsVerticalMirror(int firstRepeatedRow)
                {
                    for (var y = 0; firstRepeatedRow + y + 1 < height && firstRepeatedRow - y >= 0; y++)
                    {
                        if (!AreRowsEqual(firstRepeatedRow - y, firstRepeatedRow + y + 1))
                        {
                            return false;
                        }
                    }

                    return true;
                }

                bool AreRowsEqual(int y1, int y2)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (pattern[x, y1] != pattern[x, y2])
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
