using MazeConsole.MazeModels;

public class MazeDrawer
{
    private Maze _maze;

    public void Draw(Maze maze)
    {
        _maze = maze;
        Console.Clear();

        DrawMaze();
        DrawPlayerStatus();
        DrawMessages();
    }

    private void DrawMessages()
    {
        foreach (var message in _maze.LogMessages)
        {
            Console.WriteLine(message);
        }
    }

    private void DrawPlayerStatus()
    {
        Console.WriteLine();
        Console.WriteLine($"Player status:");
        Console.WriteLine($"Coins: {_maze.Player.Coin}");
        Console.WriteLine($"Health: {_maze.Player.CurrentHealth}");
        Console.WriteLine($"Health Potion: {_maze.Player.HealthPotion}");
    }

    private void DrawMaze()
    {
        for (int y = 0; y < _maze.Height; y++)
        {
            Console.WriteLine();

            for (int x = 0; x < _maze.Width; x++)
            {
                var cell = _maze.Player.X == x && _maze.Player.Y == y
                    ? _maze.Player
                    : _maze
                        .Cells
                        .First(cell => cell.X == x && cell.Y == y);

                var colorBefore = Console.ForegroundColor;
                Console.ForegroundColor = cell.CellColor;
                Console.Write(cell.MySymbol);
                Console.ForegroundColor = colorBefore;
            }
        }

    }
}