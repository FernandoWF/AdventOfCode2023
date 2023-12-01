namespace AdventOfCode2023.Day01
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day01\\Input.txt");

        public static object RunPart1()
        {
            var sum = 0;
            foreach (var line in lines)
            {
                var digits = line.Where(char.IsDigit);
                var number = int.Parse($"{digits.First()}{digits.Last()}");
                sum += number;
            }

            return sum;
        }

        public static object RunPart2()
        {
            var sum = 0;
            foreach (var line in lines)
            {
                var number = GetNumber(line);
                sum += number;
            }

            return sum;
        }

        private static int GetNumber(string line)
        {
            const int MaxLength = 5; // "three", "seven" and "eight"
            const int MinLength = 1; // a digit

            string? firstDigit = null;

            var i = 0;
            var length = MinLength;
            while (i < line.Length && firstDigit is null)
            {
                if (char.IsDigit(line[i]))
                {
                    firstDigit = line[i].ToString();
                    break;
                }

                if (i + length > line.Length)
                {
                    length = MinLength;
                    i++;
                }

                var word = line.Substring(i, length);
                var wordAsDigit = SwapWordForDigit(word);

                if (word != wordAsDigit)
                {
                    firstDigit = wordAsDigit;
                }

                if (length == MaxLength)
                {
                    length = MinLength;
                    i++;
                }
                else
                {
                    length++;
                }
            }

            string? lastDigit = null;
            i = line.Length - MinLength;
            length = MinLength;
            while (i >= 0 && lastDigit is null)
            {
                if (char.IsDigit(line[i]))
                {
                    lastDigit = line[i].ToString();
                    break;
                }

                if (i + length > line.Length)
                {
                    length = MinLength;
                    i--;
                }

                var word = line.Substring(i, length);
                var wordAsDigit = SwapWordForDigit(word);

                if (word != wordAsDigit)
                {
                    lastDigit = wordAsDigit;
                }

                if (length == MaxLength)
                {
                    length = MinLength;
                    i--;
                }
                else
                {
                    length++;
                }
            }

            return int.Parse($"{firstDigit}{lastDigit}");

            static string SwapWordForDigit(string word) => word switch
            {
                "one" => "1",
                "two" => "2",
                "three" => "3",
                "four" => "4",
                "five" => "5",
                "six" => "6",
                "seven" => "7",
                "eight" => "8",
                "nine" => "9",
                _ => word
            };
        }
    }
}
