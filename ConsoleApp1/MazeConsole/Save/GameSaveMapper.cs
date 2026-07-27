using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using MazeConsole.MazeModels.Intefaces;

namespace MazeConsole.Save;

// Переводит живой лабиринт в снимок (DTO) и обратно.
// С файлами не работает — только данные.
public class GameSaveMapper
{
    private readonly CellRestoreFactory _cellFactory = new();

    // Лабиринт -> снимок для JSON
    public GameSaveDto ToDto(IMaze maze)
    {
        return new GameSaveDto
        {
            Maze = new MazeSaveDto
            {
                Width = maze.Width,
                Height = maze.Height,
                Seed = maze.Seed
            },
            Player = new PlayerSaveDto
            {
                X = maze.Player.X,
                Y = maze.Player.Y,
                Coin = maze.Player.Coin,
                CurrentHealth = maze.Player.CurrentHealth,
                HealthPotion = maze.Player.HealthPotion,
                Sand = maze.Player.Sand,
                Flowers = maze.Player.Flowers,
                SnakeMeets = maze.Player.SnakeMeets
            },
            Cells = maze.Cells.Select(ToCellDto).ToList()
        };
    }

    // Снимок -> новый лабиринт с клетками и игроком
    public Maze FromDto(GameSaveDto dto)
    {
        var maze = new Maze
        {
            Width = dto.Maze.Width,
            Height = dto.Maze.Height,
            Seed = dto.Maze.Seed,
            Random = new Random(dto.Maze.Seed)
        };

        maze.Cells = dto.Cells
            .Select(cellDto => _cellFactory.Create(cellDto, maze))
            .ToList();

        maze.Player = new Player
        {
            X = dto.Player.X,
            Y = dto.Player.Y,
            Coin = dto.Player.Coin,
            CurrentHealth = dto.Player.CurrentHealth,
            HealthPotion = dto.Player.HealthPotion,
            Sand = dto.Player.Sand,
            Flowers = dto.Player.Flowers,
            SnakeMeets = dto.Player.SnakeMeets,
            MazeWhereIWasCreated = maze
        };

        return maze;
    }

    // Одна клетка -> DTO (+ доп. состояние, если оно есть)
    private static CellSaveDto ToCellDto(IBaseCell cell)
    {
        var dto = new CellSaveDto
        {
            Type = cell.GetType().Name,
            X = cell.X,
            Y = cell.Y
        };

        switch (cell)
        {
            case Coin coin:
                dto.CoinCount = coin.CoinCount;
                break;
            case MimicChest mimic:
                dto.VisitCount = mimic.VisitCount;
                break;
            case Wall wall:
                dto.Durability = wall.Durability;
                break;
            case Tree tree:
                dto.Durability = tree.Durability;
                break;
        }

        return dto;
    }
}
