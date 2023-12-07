using System.Diagnostics;

namespace AdventOfCode2023.Day07
{
    internal readonly record struct Card : IComparable<Card>
    {
        public char Label { get; }

        public Card(char label)
        {
            if (!GameRules.CardLabelsInStrengthOrder.Contains(label))
            {
                throw new ArgumentException($"Invalid label: {label}.");
            }

            Label = label;
        }

        public int CompareTo(Card other)
        {
            return GetLabelIndex(Label).CompareTo(GetLabelIndex(other.Label));

            static int GetLabelIndex(char label)
            {
                for (var i = 0; i < GameRules.CardLabelsInStrengthOrder.Count; i++)
                {
                    if (label == GameRules.CardLabelsInStrengthOrder[i])
                    {
                        return i;
                    }
                }

                throw new UnreachableException("It should be impossible to have a Card with a label not in the list.");
            }
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
