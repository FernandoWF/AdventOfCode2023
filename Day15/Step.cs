namespace AdventOfCode2023.Day15
{
    internal class Step
    {
        private readonly string lensLabel;
        private readonly int? lensFocalLength;
        private readonly int boxIndex;

        public Step(string lensLabel, int boxIndex)
        {
            this.lensLabel = lensLabel;
            this.boxIndex = boxIndex;
        }

        public Step(string lensLabel, int lensFocalLength, int boxIndex)
        {
            this.lensLabel = lensLabel;
            this.lensFocalLength = lensFocalLength;
            this.boxIndex = boxIndex;
        }

        public void Execute(Box[] boxes)
        {
            if (lensFocalLength.HasValue)
            {
                var box = boxes[boxIndex];
                var existingLensIndex = box.Lenses.FindIndex(lens => lens.Label == lensLabel);
                if (existingLensIndex >= 0)
                {
                    box.Lenses[existingLensIndex] = box.Lenses[existingLensIndex] with { FocalLength = lensFocalLength.Value };
                }
                else
                {
                    box.Lenses.Add(new Lens(lensLabel, lensFocalLength.Value));
                }
            }
            else
            {
                boxes[boxIndex].Lenses.RemoveAll(lens => lens.Label == lensLabel);
            }
        }
    }
}
