namespace AdventOfCode2023.Day08
{
    internal class Solution : ISolution
    {
        private readonly record struct Node(string Label, string LeftNodeLabel, string RightNodeLabel);
        private static readonly string[] lines = File.ReadAllLines("Day08\\Input.txt");

        public static object RunPart1()
        {
            var instructions = lines[0].ToCharArray();
            var nodes = lines[2..]
                .Select(n =>
                {
                    var parts = n.Split(" = ");
                    var label = parts[0];
                    var left = parts[1][1..4];
                    var right = parts[1][6..9];

                    return new Node(label, left, right);
                })
                .ToList();

            var instructionIndex = 0;
            Node? node = null;
            for (var i = 0; i < nodes.Count || node is null; i++)
            {
                if (nodes[i].Label == "AAA")
                {
                    node = nodes[i];
                }
            }

            var steps = 0;
            while (node!.Value.Label != "ZZZ")
            {
                var instruction = instructions[instructionIndex++];
                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }

                node = nodes.Single(n => n.Label == (instruction == 'L' ? node!.Value.LeftNodeLabel : node!.Value.RightNodeLabel));
                steps++;
            }

            return steps;
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
