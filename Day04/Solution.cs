﻿namespace AdventOfCode2023.Day04
{
    internal class Solution : ISolution
    {
        private record Scratchcard(List<int> WinningNumbers, List<int> NumbersYouHave);

        private static readonly string[] lines = File.ReadAllLines("Day04\\Input.txt");

        public static object RunPart1()
        {
            var scratchcards = ParseScratchcards();
            var totalPointsWorth = 0;

            foreach (var scratchcard in scratchcards)
            {
                var winningNumbersYouHaveCount = scratchcard.NumbersYouHave.Count(scratchcard.WinningNumbers.Contains);
                var pointsWorth = (int)Math.Pow(2, winningNumbersYouHaveCount - 1);
                totalPointsWorth += pointsWorth;
            }

            return totalPointsWorth;
        }

        private static IEnumerable<Scratchcard> ParseScratchcards()
        {
            var separatorIndex = lines[0].IndexOf('|');
            var winningNumbersStartIndex = lines[0].IndexOf(':') + 2;
            var winningNumbersEndIndex = separatorIndex - 2;
            var numbersYouHaveStartIndex = separatorIndex + 2;
            var numbersYouHaveEndIndex = lines[0].Length - 1;

            foreach (var line in lines)
            {
                var rawWinningNumbers = line[winningNumbersStartIndex..(winningNumbersEndIndex + 1)];
                var rawNumbersYouHave = line[numbersYouHaveStartIndex..(numbersYouHaveEndIndex + 1)];

                var winningNumbers = rawWinningNumbers
                    .Split(' ')
                    .Where(n => n != string.Empty)
                    .Select(int.Parse)
                    .ToList();
                var numbersYouHave = rawNumbersYouHave
                    .Split(' ')
                    .Where(n => n != string.Empty)
                    .Select(int.Parse)
                    .ToList();

                yield return new Scratchcard(winningNumbers, numbersYouHave);
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
