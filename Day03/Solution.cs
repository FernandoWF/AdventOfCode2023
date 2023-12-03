namespace AdventOfCode2023.Day03
{
    internal class Solution : ISolution
    {
        private record struct Position(int X, int Y)
        {
            public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
        }

        private record class Number(int Value, List<Position> Positions);

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
            (var symbolPositions, var numbers) = ParseSchematic();
            var partNumbers = numbers.Where(n =>
            {
                foreach (var symbolPosition in symbolPositions)
                {
                    foreach (var relativeAdjacentPosition in relativeAdjacentPosition)
                    {
                        if (n.Positions.Contains(symbolPosition + relativeAdjacentPosition))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }).ToList();

            return partNumbers.Sum(p => p.Value);
        }

        private static (List<Position> symbolsPositions, List<Number> numbers) ParseSchematic()
        {
            var symbolPositions = new List<Position>();
            var numbers = new List<Number>();

            for (var i = 0; i < lines.Length; i++)
            {
                (var symbolsInLine, var numbersInLine) = ParseLine(lines[i], i);
                symbolPositions.AddRange(symbolsInLine);
                numbers.AddRange(numbersInLine);
            }

            return (symbolPositions, numbers);

            static (List<Position> symbolPositions, List<Number> numbers) ParseLine(string line, int lineIndex)
            {
                var symbolPositions = new List<Position>();
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

                    symbolPositions.Add(new Position(i, lineIndex));
                    ParseRemainingNumber();
                }

                ParseRemainingNumber();

                return (symbolPositions, numbers);

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

        public static object RunPart2()
        {
            return null;
        }
    }
}
