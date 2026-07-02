using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using System.Collections.Concurrent;
using System.Dynamic;

public class MazeBuilder
{
    private Maze _mazeWhichWeBuildRightNow;
    private Random _random;

    public Maze BuildTestMaze(int width = 12, int height = 9, int? seed = null)
    {
        if (seed == null)
        {
            seed = DateTime.Now.Millisecond;
        }
        Console.WriteLine(seed);
        _random = new Random(seed.Value);

        _mazeWhichWeBuildRightNow = new Maze
        {
            Width = width,
            Height = height,
        };

        // var cell = _mazeWhichWeBuildRightNow.Cells.First(x => x is Wall);

        BuildWall();
        BuildGround();
        BuildCoin();
        BuildTree();
        BuildIce();
        BuildAmongus();
        BuildDiamond();
        BuildHealthPotion();
        BuildThief();
        BuildSnake();
        BuildFlower();
        BuildMimicChest();

        BuildPlayer();

        BuildPortal();
        BuildRainbow();
        BuildCrater(); //добавлен вызов метода для Ямы task-130
        return _mazeWhichWeBuildRightNow;
    }

    private void BuildPlayer()
    {
        var grounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
        var ground = GetRandomFromList(grounds);
        var player = new Player()
        {
            Coin = 1,
            Health = 3,
            MaxHealth = 3,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow,
            X = ground.X,
            Y = ground.Y,
        };

        _mazeWhichWeBuildRightNow.Player = player;
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
        var deadEnds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .Where(x => GetNearCell<Ground>(x).Count == 1)
            .ToList();
        var deadEnd = GetRandomFromList(deadEnds);

        var coin = new Coin
        {
            X = deadEnd.X,
            Y = deadEnd.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(coin);
    }

    private void BuildIce()
    {
        var ice = new Ice
        {
            X = 2,
            Y = 2,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(ice);
    }

    private void BuildGround()
    {
        var maze = _mazeWhichWeBuildRightNow;
        var cells = _mazeWhichWeBuildRightNow.Cells;

        var miner = GetRandomFromList(cells);
        var wallWhichWeCanBreak = new List<BaseCell>();

        while (true)
        {
            var nearCells = GetNearCell<Wall>(miner);
            wallWhichWeCanBreak.AddRange(nearCells);

            wallWhichWeCanBreak = wallWhichWeCanBreak
                .Where(wall => GetNearCell<Ground>(wall).Count < 2)
                .ToList();

            if (wallWhichWeCanBreak.Any() == false)
            {
                break;
            }

            miner = GetRandomFromList(wallWhichWeCanBreak);

            _mazeWhichWeBuildRightNow.ReplaceCellToGround(miner);
            wallWhichWeCanBreak.Remove(miner);

            if (wallWhichWeCanBreak.Any() == false)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Here we get near cells from maze
    /// </summary>
    /// <param name="miner">Current cell</param>
    /// <returns></returns>
    private List<BaseCell> GetNearCell<CellType>(BaseCell miner)
        where CellType : BaseCell // Get only child of BaseCell
    {
        var nearCell = _mazeWhichWeBuildRightNow
            .Cells
            .Where(cell => cell is CellType)
            .Where(cell =>
                cell.X == miner.X && cell.Y == miner.Y + 1
                || cell.X == miner.X && cell.Y == miner.Y - 1
                || cell.X == miner.X + 1 && cell.Y == miner.Y
                || cell.X == miner.X - 1 && cell.Y == miner.Y)
            .ToList();

        return nearCell;
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
    private void BuildTree()
    {
        var tree = new Tree
        {
            X = 2,
            Y = 1,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(tree);
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
    private void BuildThief()
    {
        var thief = new Thief
        {
            X = 4,
            Y = 4,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(thief);
    }

    private void BuildHealthPotion()
    {
        var wall = _mazeWhichWeBuildRightNow.Cells.FirstOrDefault(x => x is Wall);
        if (wall == null)
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

    /// <summary>
    /// Get random element from list
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    private T GetRandomFromList<T>(List<T> cells)
    {
        var randomIndex = _random.Next(cells.Count);
        var randomCell = cells[randomIndex];
        return randomCell;
    }

    private void BuildPortal()
    {
        var portal = new Portal
        {
            X = 3,
            Y = 4,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };

        _mazeWhichWeBuildRightNow.ReplaceToCell(portal);
    }


    private void BuildRainbow()
    {
        // Ставим радугу на координаты (например, x = 4, y = 2)
        var rainbow = new Rainbow
        {
            X = 4,
            Y = 2,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };

        // Метод уберет стену в этой точке и подменит её нашей радугой
        _mazeWhichWeBuildRightNow.ReplaceToCell(rainbow);
    }

    private void BuildCrater() //task-130 создан метод с ячейкой типа Яма и ее координатами
    {
        var crater = new Crater
        {
            X = 9,
            Y = 8,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow

        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(crater);
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
    private void BuildMimicChest()
    {
        var maze = _mazeWhichWeBuildRightNow;

        // Клетка считается "периметром", если она находится в крайней строке или крайнем столбце лабиринта (X == 0, X == Width-1, Y == 0, Y == Height-1).
        bool IsPerimeter(BaseCell cell) =>
            cell.X == 0 || cell.X == maze.Width - 1
            || cell.Y == 0 || cell.Y == maze.Height - 1;

        // Основной вариант: земля, которая одновременно 1) лежит на периметре лабиринта 2) стоит рядом со стеной
        var perimeterGroundsNearWall = maze
            .Cells
            .Where(x => x is Ground)
            .Where(IsPerimeter)
            .Where(x => GetNearCell<Wall>(x).Count > 0)
            .ToList();

        // Запасной вариант 1: если по периметру рядом со стеной ничего не нашлось, просто берём любую землю на периметре
        var fallbackCandidates = perimeterGroundsNearWall.Any()
            ? perimeterGroundsNearWall
            : maze.Cells.Where(x => x is Ground).Where(IsPerimeter).ToList();

        // Запасной вариант 2: если на периметре вообще нет земли (если застроили внешний контур целиком стенами), берём любую доступную землю рядом со стеной.
        if (fallbackCandidates.Any() == false)
        {
            fallbackCandidates = maze
                .Cells
                .Where(x => x is Ground)
                .Where(x => GetNearCell<Wall>(x).Count > 0)
                .ToList();
        }

        // Запасной вариант 3:  любая земля вообще
        if (fallbackCandidates.Any() == false)
        {
            fallbackCandidates = maze.Cells.Where(x => x is Ground).ToList();
        }

        var cell = GetRandomFromList(fallbackCandidates);

        var mimicChest = new MimicChest
        {
            X = cell.X,
            Y = cell.Y,
            MazeWhereIWasCreated = maze
        };
        maze.ReplaceToCell(mimicChest);
    }
}