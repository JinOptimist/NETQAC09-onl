using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;

public class MazeBuilder
{
    private Maze _maze;

    public Maze BuildTestMaze(int width = 5, int height = 5)
    {
        _maze = new Maze
        {
            Width = width,
            Height = height,
        };

        BuildWall();
        BuildGround();
        BuildCoin();

        return _maze;
    }

    private void BuildCoin()
    {
        var coin = new Coin
        {
            X = 1,
            Y = 1,
            Maze = _maze
        };
        _maze.ReplaceToCell(coin);
    }

    private void BuildGround()
    {
        for (int y = 0; y < _maze.Height; y++)
        {
            var wall = _maze.Cells.First(x => x.X == 1 && x.Y == y);
            _maze.ReplaceCellToGround(wall);
        }
    }

    private void BuildWall()
    {
        for (int y = 0; y < _maze.Height; y++)
        {
            for (int x = 0; x < _maze.Width; x++)
            {
                var cell = new Wall
                {
                    X = x,
                    Y = y,
                    Maze = _maze,
                };

                _maze.Cells.Add(cell);
            }
        }
    }
}