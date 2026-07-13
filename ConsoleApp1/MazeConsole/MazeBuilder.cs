using MazeConsole.MazeExceptions;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;

namespace MazeConsole;

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
            Seed = seed.Value,
            Random = _random
        };

        // var cell = _mazeWhichWeBuildRightNow.Cells.First(x => x is Wall);

        BuildWall();
        BuildGround();
        BuildCoin();
        BuildTree();
        BuildIce();
        BuildPileOfSand();
        BuildAmongus();
        BuildDiamond();
        BuildHealthPotion();
        BuildThief();
        BuildSnake();
        BuildFlower();

        BuildPaidDoor();

        BuildMimicChest();

        BuildPlayer();

        BuildPortal();
        BuildRainbow();
        BuildCrater(); //добавлен вызов метода для Ямы task-130
        BuildVodkaBar();
        return _mazeWhichWeBuildRightNow;
    }

    private void BuildPlayer()
    {
        // 500 is a midle of 0 - 999
        //if (_mazeWhichWeBuildRightNow.Seed < 500)
        //{
        //    throw new MazeBuildException(_mazeWhichWeBuildRightNow.Seed, "Can't crate hero");
        //}

        var grounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
        var ground = GetRandomFromList(grounds);
        var player = new Player()
        {
            Coin = 1,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow,
            X = ground.X,
            Y = ground.Y,
        };

        _mazeWhichWeBuildRightNow.Player = player;
    }

    private void BuildFlower()
    {
        var maxFlowers = Flower.MAX_FLOWERS + 1; 

        var gounds = _mazeWhichWeBuildRightNow
               .Cells
               .Where(x => x is Ground)
               .ToList();

        IBaseCell ground;

        for (int i = 0; i < maxFlowers; i++)
        {
            ground = GetRandomFromList(gounds);
            var flower = new Flower
            {
                X = ground.X,
                Y = ground.Y,
                MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
            };

            _mazeWhichWeBuildRightNow.ReplaceToCell(flower);

            gounds.Remove(ground);
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

    private void BuildGround()
    {
        var maze = _mazeWhichWeBuildRightNow;
        var cells = _mazeWhichWeBuildRightNow.Cells;

        var miner = GetRandomFromList(cells);
        var wallWhichWeCanBreak = new List<IBaseCell>();

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
    private List<IBaseCell> GetNearCell<CellType>(IBaseCell miner)
        where CellType : IBaseCell // Get only child of BaseCell
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
        var anyGrounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
        var anyGround = GetRandomFromList(anyGrounds);
        var amongus = new Amongus(_random)
        {
            X = anyGround.X,
            Y = anyGround.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(amongus);
    }
    private void BuildThief()
    {
        var thiefPossiblePositions = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
        var thiefPosition = GetRandomFromList(thiefPossiblePositions);

        var thief = new Thief
        {
            X = thiefPosition.X,
            Y = thiefPosition.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(thief);
    }

    private void BuildHealthPotion()
    {
        var randomGrounds = _mazeWhichWeBuildRightNow
             .Cells
             .Where(x => x is Ground)
             .ToList();
        var randomGround = GetRandomFromList(randomGrounds);

        var healthPotion = new HealthPotion
        {
            X = randomGround.X,
            Y = randomGround.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(healthPotion);
    }

    private void BuildSnake()
    {

        var grounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground && GetNearCell<Wall>(x).Count < 3)
            .ToList();
        var snakenest = GetRandomFromList(grounds);
        var snake = new Snake
        {
            X = snakenest.X,
            Y = snakenest.Y,
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

    private void BuildCrater() //метод с ячейкой типа Яма и произвольным выбором координат
    {
        var safeWalls = _mazeWhichWeBuildRightNow.Cells
            .Where(cell => cell is Wall && cell.Y < _mazeWhichWeBuildRightNow.Height - 1)
            .ToList(); //список ячеек Стена, которые можно использовать для проваливания вниз

        var safeWall = GetRandomFromList(safeWalls);

        var crater = new Crater
        {
            X = safeWall.X,
            Y = safeWall.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };

        _mazeWhichWeBuildRightNow.ReplaceToCell(crater);
    }

    private void BuildIce()
    {
        var crossCenters = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .Where(x => GetNearCell<Ground>(x).Count == 3)
            .ToList();
        var crossCenter = GetRandomFromList(crossCenters);

        var ice = new Ice
        {
            X = crossCenter.X,
            Y = crossCenter.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(ice);
    }

    private void BuildPileOfSand()
    {
        var grounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
        var ground = GetRandomFromList(grounds);
        var pileOfSand = new PileOfSand
        {
            X = ground.X,
            Y = ground.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(pileOfSand);
    }

    private void BuildDiamond()
    {
        var grounds = _mazeWhichWeBuildRightNow
            .Cells
            .Where(x => x is Ground)
            .ToList();
               
        var deadEnds = grounds
            .Where(x => GetNearCell<Ground>(x).Count == 1)
            .ToList();

        // если свободных тупиков нет - берём случайную клетку земли (запасной вариант)
        var placeForDiamond = deadEnds.Any()
            ? GetRandomFromList(deadEnds)
            : GetRandomFromList(grounds);

        var diamond = new Diamond(_random)
        {
            X = placeForDiamond.X,
            Y = placeForDiamond.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        _mazeWhichWeBuildRightNow.ReplaceToCell(diamond);
    }

    private void BuildVodkaBar()
    {
        // не создаем абы где, берем любую землю, чтобы не затереть другие элементы
        var grounds = _mazeWhichWeBuildRightNow.Cells.OfType<Ground>().ToList();
        if (grounds.Any())
        {
            var spot = GetRandomFromList(grounds);
            var bar = new VodkaBar
            {
                X = spot.X,
                Y = spot.Y,
                MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
            };
            _mazeWhichWeBuildRightNow.ReplaceToCell(bar);
        }
    }


    private void BuildPaidDoor()
    {   //берем все
        var wallsNearGround = _mazeWhichWeBuildRightNow.Cells
            //фильтруем стены
            .Where(cell => cell is Wall)
            //проверяем рядом землю
            .Where(cell => GetNearCell<Ground>(cell).Any()
            )
            .ToList();
        //выбираем стену из списка
        var wall = GetRandomFromList(wallsNearGround);

        //ставим
        var paidDoor = new PaidDoor
        {
            X = wall.X,
            Y = wall.Y,
            MazeWhereIWasCreated = _mazeWhichWeBuildRightNow
        };
        //меняем
        _mazeWhichWeBuildRightNow.ReplaceToCell(paidDoor);
    }
    private void BuildMimicChest()
    {
        var maze = _mazeWhichWeBuildRightNow;

        // Клетка считается "периметром", если она находится в крайней строке или крайнем столбце лабиринта (X == 0, X == Width-1, Y == 0, Y == Height-1).
        bool IsPerimeter(IBaseCell cell) =>
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
