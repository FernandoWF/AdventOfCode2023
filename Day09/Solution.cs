namespace AdventOfCode2023.Day09
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day09\\Input.txt");

        public static object RunPart1()
        {
            static int getAllZerosSequenceReturnValue(List<int> sequence) => sequence[^1];
            static int getOtherSequencesReturnValue(List<int> sequence, int nextValue) => nextValue + sequence[^1];

            return CalculateExtrapolatedValuesSum(getAllZerosSequenceReturnValue, getOtherSequencesReturnValue);
        }

        private static int CalculateExtrapolatedValuesSum(Func<List<int>, int> getAllZerosSequenceReturnValue, Func<List<int>, int, int> getOtherSequencesReturnValue)
        {
            var sequences = lines
                .Select(l => l.Split(' ').Select(int.Parse).ToList())
                .ToList();

            return sequences
                .Select(GetNextValue)
                .Sum();

            int GetNextValue(List<int> sequence)
            {
                var differenceSequence = new List<int>();

                for (var i = 1; i < sequence.Count; i++)
                {
                    var difference = sequence[i] - sequence[i - 1];
                    differenceSequence.Add(difference);
                }

                if (differenceSequence.All(n => n == 0))
                {
                    return getAllZerosSequenceReturnValue(sequence);
                }

                var differenceSequenceNextValue = GetNextValue(differenceSequence);

                return getOtherSequencesReturnValue(sequence, differenceSequenceNextValue);
            }
        }

        public static object RunPart2()
        {
            static int getAllZerosSequenceReturnValue(List<int> sequence) => sequence[0];
            static int getOtherSequencesReturnValue(List<int> sequence, int nextValue) => sequence[0] - nextValue;

            return CalculateExtrapolatedValuesSum(getAllZerosSequenceReturnValue, getOtherSequencesReturnValue);
        }
    }
}
