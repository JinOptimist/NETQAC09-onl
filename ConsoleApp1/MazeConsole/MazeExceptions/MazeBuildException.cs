namespace MazeConsole.MazeExceptions;

public class MazeBuildException : Exception
{
    public int Seed { get; private set;  }

    public MazeBuildException(int seed, string message)
        : base(message)
    {
        Seed = seed;
    }
}
