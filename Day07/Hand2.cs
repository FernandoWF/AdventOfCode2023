namespace AdventOfCode2023.Day07
{
    internal class Hand2 : IComparable<Hand2>
    {
        private const int RequiredCardQuantity = 5;

        public IReadOnlyList<Part2Card> Cards { get; private set; }
        public int Bid { get; }
        public HandType Type { get; }

        public Hand2(IReadOnlyList<Part2Card> cards, int bid)
        {
            if (cards.Count != RequiredCardQuantity)
            {
                throw new ArgumentException("Invalid card count.");
            }

            Cards = cards;
            Bid = bid;
            Type = CalculateType();
        }

        private HandType CalculateType()
        {
            var distinctLabels = new List<char>();
            var jCardsCount = Cards.Count(c => c.Label == 'J');
            var repeatedLabelsCount = jCardsCount;

            if (repeatedLabelsCount == 5)
            {
                return HandType.FiveOfAKind;
            }

            var groups = Cards
                .Where(c => c.Label != 'J')
                .GroupBy(c => c.Label)
                .OrderBy(g => g.Count());

            foreach (var group in groups)
            {
                var elements = group.ToList();

                if (!distinctLabels.Contains(group.Key))
                {
                    distinctLabels.Add(group.Key);
                }

                if (elements.Count > 1)
                {
                    repeatedLabelsCount += elements.Count;
                }
            }

            if (jCardsCount != 0 && repeatedLabelsCount == jCardsCount)
            {
                repeatedLabelsCount++;
            }

            if (repeatedLabelsCount == RequiredCardQuantity && distinctLabels.Count == 1)
            {
                return HandType.FiveOfAKind;
            }

            if (repeatedLabelsCount == RequiredCardQuantity - 1 && distinctLabels.Count == 2)
            {
                return HandType.FourOfAKind;
            }

            if (distinctLabels.Count == 2)
            {
                return HandType.FullHouse;
            }

            if (repeatedLabelsCount == RequiredCardQuantity - 2 && distinctLabels.Count == 3)
            {
                return HandType.ThreeOfAKind;
            }

            if (distinctLabels.Count == 3)
            {
                return HandType.TwoPair;
            }

            if (repeatedLabelsCount == RequiredCardQuantity - 3 && distinctLabels.Count == 4)
            {
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        public int CompareTo(Hand2? other)
        {
            if (other is null)
            {
                return 1;
            }

            if (Type == other.Type)
            {
                return CompareByHighCard();
            }

            return Type.CompareTo(other.Type);

            int CompareByHighCard()
            {
                for (var i = 0; i < Cards.Count; i++)
                {
                    var comparison = Cards[i].CompareTo(other!.Cards[i]);

                    if (comparison != 0)
                    {
                        return comparison;
                    }
                }

                return 0;
            }
        }

        public static bool operator <(Hand2 left, Hand2 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Hand2 left, Hand2 right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Hand2 left, Hand2 right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Hand2 left, Hand2 right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
