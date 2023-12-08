using MathNet.Numerics;

namespace AdventOfCode2023.Day08
{
    internal class Solution : ISolution
    {
        private enum Instruction
        {
            Left,
            Right
        }

        private readonly record struct Node(string Label, string LeftNodeLabel, string RightNodeLabel);
        private static readonly string[] lines = File.ReadAllLines("Day08\\Input.txt");

        public static object RunPart1()
        {
            var instructions = ParseInstructions();
            var nodes = ParseNodes();

            var instructionIndex = 0;
            var node = GetNodesEndingWith(nodes, "AAA").Single();
            var steps = 0;

            while (node.Label != "ZZZ")
            {
                var instruction = instructions[instructionIndex++];
                if (instructionIndex >= instructions.Count)
                {
                    instructionIndex = 0;
                }

                node = GetNextNode(nodes, node, instruction);
                steps++;
            }

            return steps;
        }

        private static List<Instruction> ParseInstructions()
        {
            return lines[0]
                .ToCharArray()
                .Select(i => i == 'L' ? Instruction.Left : Instruction.Right)
                .ToList();
        }

        private static List<Node> ParseNodes()
        {
            return lines[2..]
                .Select(n =>
                {
                    var parts = n.Split(" = ");
                    var label = parts[0];
                    var left = parts[1][1..4];
                    var right = parts[1][6..9];

                    return new Node(label, left, right);
                })
                .ToList();
        }

        private static IEnumerable<Node> GetNodesEndingWith(List<Node> nodes, string pattern)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Label.EndsWith(pattern))
                {
                    yield return nodes[i];
                }
            }
        }

        private static IEnumerable<Node> GetNodesEndingWith(List<Node> nodes, char endingCharacter)
        {
            return GetNodesEndingWith(nodes, endingCharacter.ToString());
        }

        private static Node GetNextNode(List<Node> nodes, Node currentNode, Instruction instruction)
        {
            var nextLabel = instruction == Instruction.Left
                ? currentNode.LeftNodeLabel
                : currentNode.RightNodeLabel;

            return nodes.Single(n => n.Label == nextLabel);
        }

        public static object RunPart2()
        {
            var instructions = ParseInstructions();
            var allNodes = ParseNodes();
            var nodesEndingWithZ = GetNodesEndingWith(allNodes, 'Z').ToList();
            var nodes = GetNodesEndingWith(allNodes, 'A').ToList();

            var instructionIndex = 0;
            var steps = 0L;
            var nodeCycles = new (long Length, bool FinishedCalculating)[nodes.Count];

            while (!nodeCycles.All(n => n.FinishedCalculating))
            {
                var instruction = instructions[instructionIndex++];
                if (instructionIndex >= instructions.Count)
                {
                    instructionIndex = 0;
                }

                for (var i = 0; i < nodes.Count; i++)
                {
                    nodes[i] = GetNextNode(allNodes, nodes[i], instruction);

                    if (!nodeCycles[i].FinishedCalculating)
                    {
                        nodeCycles[i].Length++;
                    }

                    if (nodes[i].Label.EndsWith('Z'))
                    {
                        nodeCycles[i].FinishedCalculating = true;
                    }
                }
                steps++;
            }

            return Euclid.LeastCommonMultiple(nodeCycles.Select(n => n.Length).ToList());
        }
    }
}
