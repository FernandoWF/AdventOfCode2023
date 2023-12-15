namespace AdventOfCode2023.Day15
{
    internal class Solution : ISolution
    {
        private static readonly string text = File.ReadAllText("Day15\\Input.txt");

        public static object RunPart1()
        {
            var sequence = text.Trim().Split(',');
            var hashSum = 0u;

            foreach (var text in sequence)
            {
                hashSum += Hash(text);
            }

            return hashSum;
        }

        private static uint Hash(string text)
        {
            var currentValue = 0u;

            foreach (var character in text)
            {
                var code = (uint)character;
                currentValue += code;
                currentValue *= 17;
                currentValue %= 256;
            }

            return currentValue;
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
