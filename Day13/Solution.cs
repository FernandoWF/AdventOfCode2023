using System.Diagnostics;

namespace AdventOfCode2023.Day13
{
    internal class Solution : ISolution
    {
        private static readonly string text = File.ReadAllText("Day13\\Input.txt");

        public static object RunPart1()
        {
            return ParsePatternGrids()
                .Select(p => GetPatternNoteSummary(p))
                .Sum();
        }

        private static List<char[,]> ParsePatternGrids()
        {
            var patterns = text.Split($"{Environment.NewLine}{Environment.NewLine}");
            var patternLines = patterns
                .Select(p => p.Trim().Split($"{Environment.NewLine}"))
                .ToList();

            return patternLines
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
        }

        static int GetPatternNoteSummary(char[,] pattern, int? originalSummary = null)
        {
            var width = pattern.GetLength(0);
            var height = pattern.GetLength(1);

            var repeatedColumnIndexes = GetRepeatedColumnIndexes();
            foreach (var index in repeatedColumnIndexes)
            {
                if (IsHorizontalMirror(index) && originalSummary != index + 1)
                {
                    return index + 1;
                }
            }

            var repeatedRowIndexes = GetRepeatedRowIndexes();
            foreach (var index in repeatedRowIndexes)
            {
                if (IsVerticalMirror(index) && originalSummary != (index + 1) * 100)
                {
                    return (index + 1) * 100;
                }
            }

            return 0;

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

        public static object RunPart2()
        {
            return ParsePatternGrids()
                .Select(GetPatternNoteSummaryWithSmudge)
                .Sum();

            static int GetPatternNoteSummaryWithSmudge(char[,] pattern)
            {
                unsafe
                {
                    fixed (char* patternPointer = pattern)
                    {
                        for (var i = 0; i < pattern.Length; i++)
                        {
                            var originalSummary = GetPatternNoteSummary(pattern);

                            var originalCharacter = *(patternPointer + i);
                            *(patternPointer + i) = originalCharacter == '.' ? '#' : '.';

                            var summary = GetPatternNoteSummary(pattern, originalSummary);
                            if (summary > 0 && summary != originalSummary)
                            {
                                return summary;
                            }
                            else
                            {
                                *(patternPointer + i) = originalCharacter;
                            }
                        }
                    }
                }

                throw new UnreachableException("There should be a summary that was calculated with smudge.");
            }
        }
    }
}
