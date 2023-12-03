namespace AdventOfCode2023.Day03
{
    internal class Solution : ISolution
    {
        private record struct Position(int X, int Y)
        {
            public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
        }

        private record struct Symbol(char Character, Position Position);
        private record class Number(int Value, List<Position> Positions);
        private record class PartNumber(Symbol AdjacentSymbol, Number Number);
        private record class Gear(Symbol Symbol, PartNumber PartNumber1, PartNumber PartNumber2);


        private static readonly string[] lines = File.ReadAllLines("Day03\\Input.txt");
        private static readonly Position[] relativeAdjacentPosition = [
            new Position(-1, -1),
            new Position(-1, 0),
            new Position(-1, 1),
            new Position(0, -1),
            new Position(0, 0),
            new Position(0, 1),
            new Position(1, -1),
            new Position(1, 0),
            new Position(1, 1)
        ];

        public static object RunPart1()
        {
            (var symbols, var numbers) = ParseSchematic();
            var partNumbers = GetPartNumbers(symbols, numbers);

            return partNumbers.Sum(p => p.Number.Value);
        }

        private static (List<Symbol> symbols, List<Number> numbers) ParseSchematic()
        {
            var symbols = new List<Symbol>();
            var numbers = new List<Number>();

            for (var i = 0; i < lines.Length; i++)
            {
                (var symbolsInLine, var numbersInLine) = ParseLine(lines[i], i);
                symbols.AddRange(symbolsInLine);
                numbers.AddRange(numbersInLine);
            }

            return (symbols, numbers);

            static (List<Symbol> symbols, List<Number> numbers) ParseLine(string line, int lineIndex)
            {
                var symbols = new List<Symbol>();
                var numbers = new List<Number>();

                var parsingNumberValue = string.Empty;
                var parsingNumberPositions = new List<Position>();

                for (var i = 0; i < line.Length; i++)
                {
                    var character = line[i];
                    if (character == '.')
                    {
                        ParseRemainingNumber();

                        continue;
                    }

                    if (char.IsDigit(character))
                    {
                        parsingNumberValue += character;
                        parsingNumberPositions.Add(new Position(i, lineIndex));

                        continue;
                    }

                    symbols.Add(new Symbol(character, new Position(i, lineIndex)));
                    ParseRemainingNumber();
                }

                ParseRemainingNumber();

                return (symbols, numbers);

                void ParseRemainingNumber()
                {
                    if (parsingNumberValue.Length > 0)
                    {
                        numbers.Add(new Number(int.Parse(parsingNumberValue), [.. parsingNumberPositions]));

                        parsingNumberValue = string.Empty;
                        parsingNumberPositions.Clear();
                    }
                }
            }
        }

        private static List<PartNumber> GetPartNumbers(List<Symbol> symbols, List<Number> numbers)
        {
            var partNumbers = new List<PartNumber>();

            foreach (var number in numbers)
            {
                foreach (var symbol in symbols)
                {
                    foreach (var relativeAdjacentPosition in relativeAdjacentPosition)
                    {
                        if (number.Positions.Contains(symbol.Position + relativeAdjacentPosition) && !partNumbers.Any(p => p.AdjacentSymbol == symbol && p.Number == number))
                        {
                            partNumbers.Add(new PartNumber(symbol, number));
                        }
                    }
                }
            }

            return partNumbers;
        }

        public static object RunPart2()
        {
            (var symbols, var numbers) = ParseSchematic();
            var partNumbers = GetPartNumbers(symbols, numbers);
            const int PartNumbersNextToGear = 2;
            var gears = new List<Gear>();

            foreach (var symbol in symbols)
            {
                var adjacentPartNumbers = partNumbers
                    .Where(p => p.AdjacentSymbol == symbol)
                    .ToList();
                if (adjacentPartNumbers.Count == PartNumbersNextToGear)
                {
                    gears.Add(new Gear(symbol, adjacentPartNumbers[0], adjacentPartNumbers[1]));
                }
            }

            return gears.Sum(g => g.PartNumber1.Number.Value * g.PartNumber2.Number.Value);
        }
    }
}
