using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;

namespace MazeConsole.Save;

public class CellsList
{
    public IBaseCell Create(CellSaveDto dto, IMaze maze)
    {
        IBaseCell cell = dto.Type switch
        {
            nameof(Wall) => new Wall { Durability = dto.Durability ?? 4 },
            nameof(Ground) => new Ground(),
            nameof(Coin) => new Coin { CoinCount = dto.CoinCount ?? Coin.COINT_COUNT_INITIAL },
            nameof(Ice) => new Ice(),
            nameof(Dirt) => new Dirt(),
            nameof(PileOfSand) => new PileOfSand(),
            nameof(Snake) => new Snake(),
            nameof(Flower) => new Flower(),
            nameof(PaidDoor) => new PaidDoor(),
            nameof(MimicChest) => new MimicChest(maze.Random) { VisitCount = dto.VisitCount ?? 0 },
            nameof(Diamond) => new Diamond(maze.Random),
            nameof(HealthPotion) => new HealthPotion(),
            nameof(Amongus) => new Amongus(maze.Random),
            nameof(Thief) => new Thief(),
            nameof(Crater) => new Crater(),
            nameof(VodkaBar) => new VodkaBar(),
            nameof(Tree) => new Tree { Durability = dto.Durability ?? 5 },
            nameof(Portal) => new Portal(),
            nameof(Rainbow) => new Rainbow(),
            nameof(Starbucks) => new Starbucks(),
            _ => new Ground()
        };

        cell.X = dto.X;
        cell.Y = dto.Y;
        cell.MazeWhereIWasCreated = maze;
        return cell;
    }
}
