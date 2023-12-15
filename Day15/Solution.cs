namespace AdventOfCode2023.Day15
{
    internal class Solution : ISolution
    {
        private static readonly string text = File.ReadAllLines("Day15\\Input.txt")[0];

        public static object RunPart1()
        {
            var sequence = text.Split(',');
            var hashSum = 0;

            foreach (var text in sequence)
            {
                hashSum += Hash(text);
            }

            return hashSum;
        }

        private static int Hash(string text)
        {
            var currentValue = 0;

            foreach (var character in text)
            {
                var code = (int)character;
                currentValue += code;
                currentValue *= 17;
                currentValue %= 256;
            }

            return currentValue;
        }

        public static object RunPart2()
        {
            var sequence = text.Split(',');
            var steps = sequence
                .Select(text =>
                {
                    if (char.IsDigit(text[^1]))
                    {
                        var values = text.Split('=');
                        var lensLabel = values[0];
                        var lensFocalLength = int.Parse(values[1]);

                        return new Step(lensLabel, lensFocalLength, Hash(lensLabel));
                    }
                    else
                    {
                        var lensLabel = text[..(text.Length - 1)];

                        return new Step(lensLabel, Hash(lensLabel));
                    }
                })
                .ToList();
            var boxes = new Box[256];
            for (var i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new Box();
            }

            foreach (var step in steps)
            {
                step.Execute(boxes);
            }

            return boxes
                .Select((box, index) => (box, boxNumber: index))
                .Sum(boxTuple =>
                {
                    return boxTuple.box.Lenses
                        .Select((lens, index) => (lens, slot: index + 1))
                        .Sum((tuple) => (boxTuple.boxNumber + 1) * tuple.slot * tuple.lens.FocalLength);
                });
        }
    }
}
