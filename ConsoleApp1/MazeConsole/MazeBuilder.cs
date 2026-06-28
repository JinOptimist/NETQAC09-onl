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
        BuildVodkaBar();
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
    
    // ячейка водка-бар
    private void BuildVodkaBar()
    {
        // находим все клетки земли, которые сейчас есть в лабиринте,
        // размещаться бар будет на земле, чтоб не затереть чужую уникальную точку
        var groundCells = _mazeWhichWeBuildRightNow.Cells
            .Where(x => x is Ground)
            .ToList();
        
        if (groundCells.Count > 0)
        {
            var random = new Random();
            var luckyGround = groundCells[random.Next(groundCells.Count)];

            var vodka = new VodkaBar
            {
                X = luckyGround.X,
                Y = luckyGround.Y,
                MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
            };
        
            // Заменяем эту единственную клетку на наш бар
            _mazeWhichWeBuildRightNow.ReplaceToCell(vodka);
        }
    }
}