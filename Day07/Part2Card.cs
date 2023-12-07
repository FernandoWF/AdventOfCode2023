namespace AdventOfCode2023.Day07
{
    internal readonly record struct Part2Card : IComparable<Part2Card>
    {
        private static readonly List<char> allCardsInStrengthOrder = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];

        public char Label { get; init; }

        public Part2Card(char label)
        {
            if (!allCardsInStrengthOrder.Contains(label))
            {
                throw new ArgumentException($"Invalid label: {label}.");
            }

            Label = label;
        }

        public int CompareTo(Part2Card other)
        {
            return allCardsInStrengthOrder.IndexOf(Label)
                .CompareTo(allCardsInStrengthOrder.IndexOf(other.Label));
        }

        public static bool operator <(Part2Card left, Part2Card right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Part2Card left, Part2Card right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Part2Card left, Part2Card right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Part2Card left, Part2Card right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
