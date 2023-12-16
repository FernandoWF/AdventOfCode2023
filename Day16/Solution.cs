namespace AdventOfCode2023.Day16
{
    internal class Solution : ISolution
    {
        [Flags]
        private enum BeamDirection
        {
            None = 0,
            Upward = 1,
            Downward = 2,
            Leftward = 4,
            Rightward = 8
        }

        private readonly record struct Beam(int CurrentTileX, int CurrentTileY, BeamDirection CurrentDirection);

        private static readonly string[] lines = File.ReadAllLines("Day16\\Input.txt");

        public static object RunPart1()
        {
            var height = lines.Length;
            var width = lines[0].Length;
            var baseGrid = new char[width, height];
            var beamDirectionGrid = new BeamDirection[width, height];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    baseGrid[x, y] = lines[y][x];
                    beamDirectionGrid[x, y] = BeamDirection.None;
                }
            }

            var beams = new List<Beam> { new(0, 0, BeamDirection.Rightward) };

            do
            {
                for (var i = 0; i < beams.Count; i++)
                {
                    var currentTileX = beams[i].CurrentTileX;
                    var currentTileY = beams[i].CurrentTileY;
                    var currentDirection = beams[i].CurrentDirection;

                    if (currentTileX < 0 || currentTileX >= width
                        || currentTileY < 0 || currentTileY >= height
                        || beamDirectionGrid[currentTileX, currentTileY].HasFlag(currentDirection))
                    {
                        beams.RemoveAt(i--);
                        continue;
                    }

                    beamDirectionGrid[currentTileX, currentTileY] |= currentDirection;

                    Action tileAction = baseGrid[currentTileX, currentTileY] switch
                    {
                        '/' when currentDirection is BeamDirection.Upward => () => { currentDirection = BeamDirection.Rightward; },
                        '/' when currentDirection is BeamDirection.Downward => () => { currentDirection = BeamDirection.Leftward; },
                        '/' when currentDirection is BeamDirection.Leftward => () => { currentDirection = BeamDirection.Downward; },
                        '/' when currentDirection is BeamDirection.Rightward => () => { currentDirection = BeamDirection.Upward; },

                        '\\' when currentDirection is BeamDirection.Upward => () => { currentDirection = BeamDirection.Leftward; },
                        '\\' when currentDirection is BeamDirection.Downward => () => { currentDirection = BeamDirection.Rightward; },
                        '\\' when currentDirection is BeamDirection.Leftward => () => { currentDirection = BeamDirection.Upward; },
                        '\\' when currentDirection is BeamDirection.Rightward => () => { currentDirection = BeamDirection.Downward; },

                        '|' when currentDirection is BeamDirection.Leftward or BeamDirection.Rightward => () =>
                        {
                            currentDirection = BeamDirection.Downward;
                            beams.Add(new Beam(currentTileX, currentTileY - 1, BeamDirection.Upward));
                        },

                        '-' when currentDirection is BeamDirection.Upward or BeamDirection.Downward => () =>
                        {
                            currentDirection = BeamDirection.Rightward;
                            beams.Add(new Beam(currentTileX - 1, currentTileY, BeamDirection.Leftward));
                        },

                        _ => () => { /* Do nothing */ }
                    };
                    tileAction();

                    Action moveAction = currentDirection switch
                    {
                        BeamDirection.Upward => () => { currentTileY--; },
                        BeamDirection.Downward => () => { currentTileY++; },
                        BeamDirection.Leftward => () => { currentTileX--; },
                        BeamDirection.Rightward => () => { currentTileX++; },

                        _ => () => { }
                    };
                    moveAction();

                    beams[i] = new Beam(currentTileX, currentTileY, currentDirection);
                }
            }
            while (beams.Count > 0);

            var energizedTiles = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (beamDirectionGrid[x, y] != BeamDirection.None)
                    {
                        energizedTiles++;
                    }
                }
            }

            return energizedTiles;
        }

        public static object RunPart2()
        {
            return null;
        }
    }
}
