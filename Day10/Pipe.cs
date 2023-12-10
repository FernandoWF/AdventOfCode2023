using System.Diagnostics;

namespace AdventOfCode2023.Day10
{
    internal readonly record struct Pipe(int X, int Y, char Shape)
    {
        public bool ConnectsTo(Pipe pipe)
        {
            return Shape switch
            {
                'F' => pipe.Shape switch
                {
                    '-' or '7' => (pipe.X, pipe.Y) == (X + 1, Y),
                    '|' or 'L' => (pipe.X, pipe.Y) == (X, Y + 1),
                    'J' => (pipe.X, pipe.Y) == (X + 1, Y) || (pipe.X, pipe.Y) == (X, Y + 1),
                    _ => false
                },
                '-' => pipe.Shape switch
                {
                    'F' or 'L' => (pipe.X, pipe.Y) == (X - 1, Y),
                    '-' => (pipe.X, pipe.Y) == (X - 1, Y) || (pipe.X, pipe.Y) == (X + 1, Y),
                    '7' or 'J' => (pipe.X, pipe.Y) == (X + 1, Y),
                    _ => false
                },
                '7' => pipe.Shape switch
                {
                    'F' or '-' => (pipe.X, pipe.Y) == (X - 1, Y),
                    '|' or 'J' => (pipe.X, pipe.Y) == (X, Y + 1),
                    'L' => (pipe.X, pipe.Y) == (X - 1, Y) || (pipe.X, pipe.Y) == (X, Y + 1),
                    _ => false
                },
                '|' => pipe.Shape switch
                {
                    'F' or '7' => (pipe.X, pipe.Y) == (X, Y - 1),
                    '|' => (pipe.X, pipe.Y) == (X, Y - 1) || (pipe.X, pipe.Y) == (X, Y + 1),
                    'J' or 'L' => (pipe.X, pipe.Y) == (X, Y + 1),
                    _ => false
                },
                'J' => pipe.Shape switch
                {
                    'F' => (pipe.X, pipe.Y) == (X, Y - 1) || (pipe.X, pipe.Y) == (X - 1, Y),
                    '-' or 'L' => (pipe.X, pipe.Y) == (X - 1, Y),
                    '7' or '|' => (pipe.X, pipe.Y) == (X, Y - 1),
                    _ => false
                },
                'L' => pipe.Shape switch
                {
                    'F' or '|' => (pipe.X, pipe.Y) == (X, Y - 1),
                    '-' or 'J' => (pipe.X, pipe.Y) == (X + 1, Y),
                    '7' => (pipe.X, pipe.Y) == (X, Y - 1) || (pipe.X, pipe.Y) == (X + 1, Y),
                    _ => false
                },
                _ => throw new UnreachableException("There should not be any other kind of pipe shape.")
            };
        }
    }
}
