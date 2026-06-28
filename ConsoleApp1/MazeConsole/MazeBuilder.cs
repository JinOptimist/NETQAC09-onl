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
        BuildAmongus();
        BuildDiamond();
        return _mazeWhichWeBuildRightNow;
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
    private void BuildAmongus()
    {
        var amongus = new Amongus
        {
            X = 6,
            Y = 7,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(amongus);
    }
    private void BuildDiamond()
    {
        var diamond = new Diamond
        {
            X = 8,
            Y = 8,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(diamond);
    }
}

