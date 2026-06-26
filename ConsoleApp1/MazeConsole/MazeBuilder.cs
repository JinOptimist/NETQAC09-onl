using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;

public class MazeBuilder
{
    private Maze _mazeWhichWeBuildRightNow;

    public Maze BuildTestMaze(int width = 12, int height = 9)
    {
        _mazeWhichWeBuildRightNow = new Maze
        {
            Width = width,
            Height = height,
        };

        // var cell = _mazeWhichWeBuildRightNow.Cells.First(x => x is Wall);

        BuildWall();
        BuildGround();
        BuildCoin();
        BuildFlower();

        return _mazeWhichWeBuildRightNow;
    }

    private void BuildFlower()
    {
        for (int y = 0; y < _mazeWhichWeBuildRightNow.Height; y++)
        {
            for (int x = 0; x < _mazeWhichWeBuildRightNow.Width; x++)
            {
                var cell = _mazeWhichWeBuildRightNow.Cells.First(c => c.X == x && c.Y == y);
                if (cell is not Wall)
                {
                    continue;
                }

                var flower = new Flower
                {
                    X = x,
                    Y = y,
                    MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
                };
                _mazeWhichWeBuildRightNow.ReplaceToCell(flower);

                return;
            }
        }
    }

    private void BuildCoin()
    {
        var coin = new Coin
        {
            X = 1,
            Y = 1,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(coin);
    }

    private void BuildGround()
    {
        for (int y = 0; y < _mazeWhichWeBuildRightNow.Height; y++)
        {
            var wall = _mazeWhichWeBuildRightNow.Cells.First(x => x.X == 1 && x.Y == y);
            _mazeWhichWeBuildRightNow.ReplaceCellToGround(wall);
        }
    }

    private void BuildWall()
    {
        for (int y = 0; y < _mazeWhichWeBuildRightNow.Height; y++)
        {
            for (int x = 0; x < _mazeWhichWeBuildRightNow.Width; x++)
            {
                var cell = new Wall
                {
                    X = x,
                    Y = y,
                    MazeWhereIWasCreated = _mazeWhichWeBuildRightNow,
                };

                _mazeWhichWeBuildRightNow.Cells.Add(cell);
            }
        }
    }
}