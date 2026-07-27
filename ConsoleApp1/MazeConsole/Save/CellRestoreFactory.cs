using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;

namespace MazeConsole.Save;

// По строке Type из сейва создаёт нужную клетку и восстанавливает её состояние
public class CellRestoreFactory
{
    public IBaseCell Create(CellSaveDto dto, IMaze maze)
    {
        // Type должен совпадать с именем класса при сохранении (GetType().Name)
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
            // Клетки с Random берут генератор загруженного лабиринта
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
            // Неизвестный тип — просто земля (без падения игры)
            _ => new Ground()
        };

        cell.X = dto.X;
        cell.Y = dto.Y;
        cell.MazeWhereIWasCreated = maze;
        return cell;
    }
}
