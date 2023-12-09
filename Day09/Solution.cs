namespace AdventOfCode2023.Day09
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day09\\Input.txt");

        public static object RunPart1()
        {
            var sequences = lines
                .Select(l => l.Split(' ').Select(int.Parse).ToList())
                .ToList();
            var nextValues = sequences
                .Select(GetNextValue);

            return nextValues.Sum();

            static int GetNextValue(List<int> sequence)
            {
                var differenceSequence = new List<int>();

                for (var i = 1; i < sequence.Count; i++)
                {
                    var difference = sequence[i] - sequence[i - 1];
                    differenceSequence.Add(difference);
                }

                if (differenceSequence.All(n => n == 0))
                {
                    return sequence[^1];
                }

                var differenceSequenceNextValue = GetNextValue(differenceSequence);

                return differenceSequenceNextValue + sequence[^1];
            }
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
