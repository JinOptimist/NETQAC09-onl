using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeExceptions
{
    public class IceCellExceptions : Exception
    {
        public int Seed { get; }
        public int SteppedX { get; }
        public int SteppedY { get; }

        public IceCellExceptions(int seed, int x, int y, string message) : base(message)
        {
            Seed = seed;
            SteppedX = x;
            SteppedY = y;
        }
}
}
