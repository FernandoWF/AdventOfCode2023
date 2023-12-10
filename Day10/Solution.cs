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

            return MapPipeToConnections(map).Count / 2;
        }

        private static Dictionary<Pipe, HashSet<Pipe>> MapPipeToConnections(char[][] map)
        {
            var mapLength = map[0].Length;
            Pipe startingPipe = default;

            for (var y = 0; y < map.Length && startingPipe == default; y++)
            {
                for (var x = 0; x < map.Length && startingPipe == default; x++)
                {
                    if (map[y][x] == 'S')
                    {
                        map[y][x] = StartingPipeCharacter;
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

            return pipeToConnections;
        }

        public static object RunPart2()
        {
            var map = lines
                .Select(l => l.ToCharArray())
                .ToArray();
            var pipeToConnections = MapPipeToConnections(map);

            var lowerX = pipeToConnections.Keys.MinBy(p => p.X).X;
            var higherX = pipeToConnections.Keys.MaxBy(p => p.X).X;
            var lowerY = pipeToConnections.Keys.MinBy(p => p.Y).Y;
            var higherY = pipeToConnections.Keys.MaxBy(p => p.Y).Y;

            var insideTileCount = 0;

            for (var y = lowerY; y <= higherY; y++)
            {
                for (var x = lowerX; x <= higherX; x++)
                {
                    var character = map[y][x];
                    var shouldCheckCharacter = character == '.' || !pipeToConnections.ContainsKey(new Pipe(x, y, character));

                    if (shouldCheckCharacter && IsInside(x, y))
                    {
                        insideTileCount++;
                    }
                }
            }

            return insideTileCount;

            bool IsInside(int tileX, int tileY)
            {
                var isInside = false;

                for (int y = tileY + 1, x = tileX + 1; y <= higherY && x <= higherX; y++, x++)
                {
                    var character = map[y][x];

                    if (character != '.' && character != 'L' && character != '7')
                    {
                        if (pipeToConnections.ContainsKey(new Pipe(x, y, character)))
                        {
                            isInside = !isInside;
                        }
                    }
                }

                return isInside;
            }
        }
    }
}
