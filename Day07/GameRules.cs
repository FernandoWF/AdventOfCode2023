namespace AdventOfCode2023.Day07
{
    internal static class GameRules
    {
        public static CamelCardsRules Current { get; set; } = CamelCardsRules.Default;

        public static IReadOnlyList<char> CardLabelsInStrengthOrder => Current == CamelCardsRules.Default
            ? defaultRulesCardLabelsInStrengthOrder
            : jokerRulesCardLabelsInStrengthOrder;

        private static readonly IReadOnlyList<char> defaultRulesCardLabelsInStrengthOrder = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
        private static readonly IReadOnlyList<char> jokerRulesCardLabelsInStrengthOrder = [JokerCardLabel, '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];

        public const char JokerCardLabel = 'J';
    }
}
