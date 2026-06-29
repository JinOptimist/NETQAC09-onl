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
        BuildHealthPotion();
        BuildSnake();
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

    private void BuildHealthPotion()
    {
        var wall = _mazeWhichWeBuildRightNow.Cells.FirstOrDefault(x => x is Wall);
        if (wall  == null)
        {
            return;
        }

        var healthPotion = new HealthPotion
        {
            X = wall.X,
            Y = wall.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(healthPotion);
    }

    private void BuildSnake()
    {
        var snake = new Snake
        {
            X = 5,
            Y = 3,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow

        };
            _mazeWhichWeBuildRightNow.ReplaceToCell(snake);
    }
}