namespace AdventOfCode2023.Day07
{
    internal enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    internal class Hand : IComparable<Hand>
    {
        private const int RequiredCardQuantity = 5;

        public IReadOnlyList<Card> Cards { get; }
        public int Bid { get; }
        public HandType Type { get; }

        public Hand(IReadOnlyList<Card> cards, int bid)
        {
            if (cards.Count != RequiredCardQuantity)
            {
                throw new ArgumentException("Invalid card count.");
            }

            Cards = cards;
            Bid = bid;
            Type = CalculateType();
            Console.WriteLine($"{string.Join(string.Empty, Cards.Select(c => c.Label))} - {Type}");
        }

        private HandType CalculateType()
        {
            var repeatedLabelsCount = 0;
            var distinctLabels = new List<char>();

            foreach (var group in Cards.GroupBy(c => c.Label).ToList())
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

        public int CompareTo(Hand? other)
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

        public static bool operator <(Hand left, Hand right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Hand left, Hand right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Hand left, Hand right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Hand left, Hand right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
