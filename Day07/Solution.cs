﻿namespace AdventOfCode2023.Day07
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day07\\Input.txt");

        public static object RunPart1()
        {
            return CalculateTotalWinnings();
        }

        private static int CalculateTotalWinnings()
        {
            var hands = ParseHands();
            hands.Sort();

            return hands
                .Select((hand, index) => (hand.Bid, Rank: index + 1))
                .Sum(tuple => tuple.Bid * tuple.Rank);
        }

        private static List<Hand> ParseHands() => lines
            .Select(l =>
            {
                var split = l.Split(' ');
                var cards = split[0]
                    .Select(c => new Card(c))
                    .ToList();
                var bid = int.Parse(split[1]);

                return new Hand(cards, bid);
            })
            .ToList();

        public static object RunPart2()
        {
            GameRules.Current = CamelCardsRules.Joker;
            return CalculateTotalWinnings();
        }
    }
}
