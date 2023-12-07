namespace AdventOfCode2023.Day07
{
    internal readonly record struct Card : IComparable<Card>
    {
        private static readonly List<char> allCardsInStrengthOrder = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];

        public char Label { get; }

        public Card(char label)
        {
            if (!allCardsInStrengthOrder.Contains(label))
            {
                throw new ArgumentException($"Invalid label: {label}.");
            }

            Label = label;
        }

        public int CompareTo(Card other)
        {
            return allCardsInStrengthOrder.IndexOf(Label)
                .CompareTo(allCardsInStrengthOrder.IndexOf(other.Label));
        }

        public static bool operator <(Card left, Card right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Card left, Card right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Card left, Card right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Card left, Card right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
