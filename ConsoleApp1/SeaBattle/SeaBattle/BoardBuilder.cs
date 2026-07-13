using SeaBattleConsole.BoardTest;
using SeaBattleConsole.SeaBattleModels;
using SeaBattleConsole.SeaBattleModels.Cells;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
namespace SeaBattleConsole;

public class BoardBuilder
{
    private Board boardWhichWeBuildRightNowPlayerOne;
    private Board boardWhichWeBuildRightNowPlayerTwo;
    private Random _random;
    private const int NumberOfFourSizeShips = 1;
    private const int NumberOfThreeSizeShips = 2;
    private const int NumberOfTwoSizeShips = 3;
    private const int NumberOfOneSizeShips = 4;
    private const int FourSize = 4;
    private const int ThreeSize = 3;
    private const int TwoSize = 2;
    private const int OneSize = 1;
    private const int NumberOfAttempts = 4; // Вверх вниз влево вправо

    public Tuple<Board,Board, List<Ship>, List<Ship>> BuildTestBoard(int width = 10, int height = 10, int? seed = null)
    {
        if (seed == null)
        {
            seed = DateTime.Now.Millisecond;
        }
        _random = new Random(seed.Value);

        boardWhichWeBuildRightNowPlayerOne = new Board
        {
            Width = width,
            Height = height,
            Seed = seed.Value,
            Random = _random
        };
        boardWhichWeBuildRightNowPlayerTwo = new Board
        {
            Width = width,
            Height = height,
            Seed = seed.Value,
            Random = _random
        };
        BuildWater(boardWhichWeBuildRightNowPlayerOne);
        BuildWater(boardWhichWeBuildRightNowPlayerTwo);
        var playerOneShips = BuildArmy(boardWhichWeBuildRightNowPlayerOne, _random, CellType.player1); ;
        var playerTwoShips = BuildArmy(boardWhichWeBuildRightNowPlayerTwo, _random, CellType.player2); ;
        var answer = Tuple.Create(
            boardWhichWeBuildRightNowPlayerOne,
            boardWhichWeBuildRightNowPlayerTwo,
            playerOneShips,
            playerTwoShips
        );
        return answer;
    }
    private void BuildWater(Board board)
    {
        for (int y = 0; y < board.Height; y++)
        {
            for (int x = 0; x < board.Width; x++)
            {
                var cell = new Water
                {
                    X = x,
                    Y = y,
                    BoardWhereIWasCreated = board
                };
                board.Cells.Add(cell);
            }
        }
    }
    private Ship BuildShip(Board board, Random random, int shipType, CellType whoToBuildFor)
    {
        var ShipToReturn = new Ship(shipType, whoToBuildFor);
        while (true)
        {
            var xRandom = random.Next(0, 10);
            var yRandom = random.Next(0, 10);
            var startCell = board.Cells.First(x => x.X == xRandom
            && x.Y == yRandom);
            var resultOfNavigation = CheckNavigation(board,startCell, shipType);
            if (resultOfNavigation != Direction.None)
            {
                ShipToReturn = ReplaceWaterToShip(board,startCell,shipType, resultOfNavigation,whoToBuildFor);
                return ShipToReturn;
            }
            else
            {
                continue;
            }
        }

    }
    private List<Ship> BuildArmy(Board board, Random random, CellType WhoToBuildFor)
    {
        var ListToReturn = new List<Ship>();
        ListToReturn.AddRange(BuildShips(board, random, NumberOfFourSizeShips, FourSize, WhoToBuildFor));
        ListToReturn.AddRange(BuildShips(board, random, NumberOfThreeSizeShips, ThreeSize, WhoToBuildFor));
        ListToReturn.AddRange(BuildShips(board, random, NumberOfTwoSizeShips, TwoSize, WhoToBuildFor));
        ListToReturn.AddRange(BuildShips(board, random, NumberOfOneSizeShips, OneSize, WhoToBuildFor));
        return ListToReturn;
    }
    private List<Ship> BuildShips(Board board, Random random, int numberOfShips, int shipType, CellType WhoToBuildFor)
    {
        var ShipsCreated = 0;
        var ListToReturn = new List<Ship>();
        while (ShipsCreated < numberOfShips)
        {
            ListToReturn.Add(BuildShip(board, random,shipType,WhoToBuildFor));
            ShipsCreated++;
        }
        return ListToReturn;
    }
    private bool CheckCell(Board board, int inputX, int inputY)
    {
        //int xRangeLeft = x - 1;
        //int yRangeUp = y - 1;
        //int xRangeRight = x + 1;
        //int yRangeDown = y + 1;
        //if (xRangeLeft < 0)
        //{
        //    xRangeLeft = 0;
        //}
        //if (xRangeRight > 9)
        //{
        //    xRangeRight = 9;
        //}
        //if (yRangeUp < 0)
        //{
        //    yRangeUp = 0;
        //}
        //if (yRangeDown > 9)
        //{
        //    yRangeDown = 9;
        //}
        //for (int i = xRangeLeft; i <= xRangeRight; i++)
        //{
        //    for (int j = yRangeUp; j <= yRangeDown; j++)
        //    {
        //        var cellToTest = _boardWhichWeBuildRightNow.Cells.First(x => x.X == i && x.Y == j);
        //        if (cellToTest.IsAvailable == false) // ???
        //        {
        //            return false;
        //        }
                
        //    }
        //}
        
        return board.Cells.First(x => x.X == inputX && x.Y == inputY).IsAvailable;
    }
    private Direction CheckNavigation(Board board, BaseCell cellTesting, int ShipType) // Возвращаем int куда строить, 0 Вверх 1 вниз 2 влево 3 вправо 4 Никуда
    {
        var ShipToBuild = ShipType - 1;
        int startX = cellTesting.X;
        int startY = cellTesting.Y;
        Direction[] directions =
        {
    Direction.Up,
    Direction.Down,
    Direction.Left,
    Direction.Right
};

        var randomDirections = directions.OrderBy(_ => Random.Shared.Next());
        foreach (Direction direction in randomDirections)// 0 вверх 1 вниз 2 влево 3 вправо
        {
            bool canBuild = true;
            if (direction == Direction.Up) // Проверяем сверху элементы
            {
                if (startY - ShipToBuild < 0)
                {
                    continue;
                }
                for (int i = startY; i >= startY - ShipToBuild; i--) // Проверяем сверху элементы
                {
                    if (CheckCell(board, startX, i) == false)
                    {
                        canBuild = false;
                        break;
                    }                }
                if (canBuild == true)
                {
                    return direction;
                }
            }
            if (direction == Direction.Down) // Проверяем снизу элементы
            {
                if (startY + ShipToBuild > 9) // Проверяем что места хватит
                {
                    continue;
                }
                for (int i = startY; i <= startY + ShipToBuild; i++) // // Проверяем снизу элементы
                {
                    if (CheckCell(board,startX, i) == false)
                    {
                        canBuild = false;
                        break;
                    }        
                }
                if (canBuild == true)
                {
                    return direction;
                }
            }
            if (direction == Direction.Left) // Проверяем слева элементы
            {
                if (startX - ShipToBuild < 0) // Проверяем что места хватит
                {
                    continue;
                }
                for (int i = startX; i >= startX - ShipToBuild; i--) // // Проверяем слева элементы
                {
                    if (CheckCell(board,i, startY) == false)
                    {
                        canBuild = false;
                        break;
                    }
                }
                if (canBuild == true)
                {
                    return direction;
                }
            }
            if (direction == Direction.Right) // Проверяем справа элементы
            {
                if (startX + ShipToBuild > 9) // Проверяем что места хватит
                {
                    continue;
                }
                for (int i = startX; i <= startX + ShipToBuild; i++) // // Проверяем справа элементы
                {
                    if (CheckCell(board,i, startY) == false)
                    {
                        canBuild = false;
                        break;
                    }
                }
                if (canBuild == true)
                {
                    return direction;
                }
            }
        }
        return Direction.None; // Если не нашли, возвращаем номер 4, значит не нашли
    }
    private Ship ReplaceWaterToShip(Board board, BaseCell cellToStart, int shipType, Direction navigation, CellType whoToBuildFor) // Возвращаем корабль
    {
        var shipToBuild = shipType;
        var cellToIterate = cellToStart;
        var xStart = cellToStart.X;
        var yStart = cellToStart.Y;
        var ShipToReturn = new Ship(shipType,whoToBuildFor);
        var shipPartList = new List<ShipCell>();  
        if (navigation == Direction.Up) // Будем строить вверх
        {
            for (int i = yStart; i > yStart - shipToBuild; i--)
            {
                cellToIterate = board.Cells.First(x => x.X == xStart && x.Y == i);
                BuildShip(board, cellToIterate, whoToBuildFor, ShipToReturn, xStart, i);
                //var shipPartToAdd = board.ReplaceCellToShip(cellToIterate, whoToBuildFor);
                //ShipToReturn.ShipParts.Add(shipPartToAdd);
                //MakeCellsNotAvailable(board,xStart, i);

            }
        }
        if (navigation == Direction.Down) // Будем строить вниз
        {
            for (int i = yStart; i < yStart + shipToBuild; i++)
            {
                cellToIterate = board.Cells.First(x => x.X == xStart && x.Y == i);
                BuildShip(board, cellToIterate,whoToBuildFor,ShipToReturn,xStart,i);
                //var shipPartToAdd = board.ReplaceCellToShip(cellToIterate, whoToBuildFor);
                //ShipToReturn.ShipParts.Add(shipPartToAdd);
                //MakeCellsNotAvailable(board,xStart, i);
            }
        }
        if (navigation == Direction.Left) // Будем строить влево
        {
            for (int i = xStart; i > xStart - shipToBuild; i--)
            {
                cellToIterate = board.Cells.First(x => x.X == i && x.Y == yStart);
                BuildShip(board, cellToIterate, whoToBuildFor, ShipToReturn, i, yStart);
                //var shipPartToAdd = board.ReplaceCellToShip(cellToIterate, whoToBuildFor);
                //ShipToReturn.ShipParts.Add(shipPartToAdd);
                //MakeCellsNotAvailable(board,i, yStart);
            }
        }
        if (navigation == Direction.Right) // Будем строить вправо
        {
            for (int i = xStart; i < xStart + shipToBuild; i++)
            {
                cellToIterate = board.Cells.First(x => x.X == i && x.Y == yStart);
                BuildShip(board, cellToIterate, whoToBuildFor, ShipToReturn, i, yStart);
                //var shipPartToAdd = board.ReplaceCellToShip(cellToIterate, whoToBuildFor);
                //ShipToReturn.ShipParts.Add(shipPartToAdd);
                //MakeCellsNotAvailable(board,i, yStart);

            }
        }
        return ShipToReturn;
    }
    private void MakeCellsNotAvailable(Board board, int x, int y)
    {
        int xRangeLeft = x - 1;
        int yRangeUp = y - 1;
        int xRangeRight = x + 1;
        int yRangeDown = y + 1;
        if (xRangeLeft < 0)
        {
            xRangeLeft = 0;
        }
        if (xRangeRight > 9)
        {
            xRangeRight = 9;
        }
        if (yRangeUp < 0)
        {
            yRangeUp = 0;
        }
        if (yRangeDown > 9)
        {
            yRangeDown = 9;
        }
        for (int i = xRangeLeft; i <= xRangeRight; i++)
        {
            for (int j = yRangeUp; j <= yRangeDown; j++)
            {
                board.Cells.First(x => x.X == i && x.Y == j).IsAvailable = false;
            }
        }
    }
    private void BuildShip(Board board, BaseCell cellToBuild ,CellType whoToBuildFor, Ship shipToFix, int x, int y)
    {
        var shipPartToAdd = board.ReplaceCellToShip(cellToBuild, whoToBuildFor);
        shipToFix.ShipParts.Add(shipPartToAdd);
        MakeCellsNotAvailable(board, x, y);

    }
}
