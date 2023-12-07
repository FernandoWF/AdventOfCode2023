namespace AdventOfCode2023.Day07
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day07\\Input.txt");

        public static object RunPart1()
        {
            var hands = ParseHands();
            hands.Sort();

            return hands
                .Select((hand, index) => (hand, rank: index + 1))
                .Sum(tuple => tuple.hand.Bid * tuple.rank);
        }

        public static List<Hand> ParseHands() => lines
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

        public static List<Hand2> ParseHands2() => lines
            .Select(l =>
            {
                var split = l.Split(' ');
                var cards = split[0]
                    .Select(c => new Part2Card(c))
                    .ToList();
                var bid = int.Parse(split[1]);

                return new Hand2(cards, bid);
            })
            .ToList();

        public static object RunPart2()
        {
            var hands = ParseHands2();
            hands.Sort();

            return hands
                .Select((hand, index) => (hand, rank: index + 1))
                .Sum(tuple => tuple.hand.Bid * tuple.rank);
        }
    }
}
