using System.Diagnostics;

namespace AdventOfCode2023.Day07
{

    internal class Hand : IComparable<Hand>
    {
        private enum Type
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        private const int CardCount = 5;

        private readonly Type type;

        public IReadOnlyList<Card> Cards { get; }
        public int Bid { get; }

        public Hand(IReadOnlyList<Card> cards, int bid)
        {
            if (cards.Count != CardCount)
            {
                throw new ArgumentException("Invalid card count.");
            }

            Cards = cards;
            Bid = bid;
            type = CalculateType();
        }

        private Type CalculateType()
        {
            Dictionary<char, int> labelToCardCount;

            if (GameRules.Current == CamelCardsRules.Default)
            {
                labelToCardCount = MapLabelToCardCount(Cards);
            }
            else
            {
                var jokerCardCount = Cards.Count(c => c.Label == GameRules.JokerCardLabel);
                if (jokerCardCount == CardCount)
                {
                    return Type.FiveOfAKind;
                }

                labelToCardCount = MapLabelToCardCount(Cards.Where(c => c.Label != GameRules.JokerCardLabel));
                var mostRepeatedLabel = labelToCardCount.MaxBy(m => m.Value).Key;
                labelToCardCount[mostRepeatedLabel] += jokerCardCount;
            }

            var nonRepeatingCardCount = labelToCardCount.Keys.Distinct().Count();
            var repeatingCardCount = labelToCardCount.Values.Where(c => c > 1).Sum();

            return nonRepeatingCardCount switch
            {
                CardCount => Type.HighCard,
                CardCount - 1 => Type.OnePair,
                CardCount - 2 when repeatingCardCount == 3 => Type.ThreeOfAKind,
                CardCount - 2 => Type.TwoPair,
                CardCount - 3 when repeatingCardCount == 4 => Type.FourOfAKind,
                CardCount - 3 => Type.FullHouse,
                CardCount - 4 => Type.FiveOfAKind,
                _ => throw new UnreachableException("Impossible count.")
            };

            static Dictionary<char, int> MapLabelToCardCount(IEnumerable<Card> cards)
            {
                var labelToCardCount = new Dictionary<char, int>();

                foreach (var card in cards)
                {
                    if (labelToCardCount.TryGetValue(card.Label, out int count))
                    {
                        labelToCardCount[card.Label] = ++count;
                    }
                    else
                    {
                        labelToCardCount.Add(card.Label, 1);
                    }
                }

                return labelToCardCount;
            }
        }

        public int CompareTo(Hand? other)
        {
            if (other is null)
            {
                return 1;
            }

            if (type == other.type)
            {
                return CompareByHighCard();
            }

            return type.CompareTo(other.type);

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
