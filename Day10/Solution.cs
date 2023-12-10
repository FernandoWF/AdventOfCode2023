namespace AdventOfCode2023.Day10
{
    internal class Solution : ISolution
    {
        private static readonly string[] lines = File.ReadAllLines("Day10\\Input.txt");
        private const char StartingPipeCharacter = 'F';
        private static readonly (int X, int Y)[] adjacentPositions = new[]
        {
            (0, -1),
            (0, 1),
            (-1, 0),
            (1, 0)
        };

        public static object RunPart1()
        {
            var map = lines
                .Select(l => l.ToCharArray())
                .ToArray();
            var mapLength = map[0].Length;
            Pipe startingPipe = default;

            for (var y = 0; y < map.Length && startingPipe == default; y++)
            {
                for (var x = 0; x < map.Length && startingPipe == default; x++)
                {
                    if (map[y][x] == 'S')
                    {
                        startingPipe = new Pipe(x, y, StartingPipeCharacter);
                    }
                }
            }

            var pipeToConnections = new Dictionary<Pipe, HashSet<Pipe>>();
            var pipeDiscoveryQueue = new Queue<Pipe>();
            pipeDiscoveryQueue.Enqueue(startingPipe);

            while (pipeDiscoveryQueue.Count > 0)
            {
                var pipe = pipeDiscoveryQueue.Dequeue();

                foreach (var (X, Y) in adjacentPositions)
                {
                    var adjacentX = pipe.X + X;
                    var adjacentY = pipe.Y + Y;

                    if (adjacentX >= mapLength || adjacentX < 0 || adjacentY >= mapLength || adjacentY < 0)
                    {
                        continue;
                    }

                    var connection = new Pipe(adjacentX, adjacentY, map[adjacentY][adjacentX]);

                    if (pipe.ConnectsTo(connection))
                    {
                        if (!pipeToConnections.TryGetValue(pipe, out HashSet<Pipe>? value))
                        {
                            value = [];
                            pipeToConnections.Add(pipe, value);
                        }

                        value.Add(connection);

                        if (!pipeToConnections.ContainsKey(connection))
                        {
                            pipeDiscoveryQueue.Enqueue(connection);
                        }
                    }
                }
            }

            return pipeToConnections.Count / 2;
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
