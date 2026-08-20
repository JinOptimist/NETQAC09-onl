using System.Text.Json;
using MazeConsole;
using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;
using WebAppSmile.Models;
using WebAppSmile.Services.Interfaces;

namespace WebAppSmile.Services;

public class MazeSaveService : IMazeSaveService
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true };

    private readonly IWebHostEnvironment _webHostEnvironment;

    public MazeSaveService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public void Save(MazeContoller controller, string sessionId)
    {
        var maze = controller.Maze;
        var player = maze.Player;

        var dto = new MazeSaveDto
        {
            Width = maze.Width,
            Height = maze.Height,
            Seed = maze.Seed,
            Cells = maze.Cells
                .Select(cell => new MazeSaveCellDto { X = cell.X, Y = cell.Y, Type = cell.GetType().Name })
                .ToList(),
            Player = new MazeSavePlayerDto
            {
                X = player.X,
                Y = player.Y,
                Coin = player.Coin,
                CurrentHealth = player.CurrentHealth,
                HealthPotion = player.HealthPotion,
                Sand = player.Sand,
                Flowers = player.Flowers,
                SnakeMeets = player.SnakeMeets,
            },
            Messages = maze.LogMessages.ToList(),
        };

        var json = JsonSerializer.Serialize(dto, SerializerOptions);

        var path = GetSavePath(sessionId);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, json);
    }

    public MazeContoller? Load(string sessionId)
    {
        var path = GetSavePath(sessionId);
        if (!File.Exists(path))
        {
            return null;
        }

        var json = File.ReadAllText(path);
        var dto = JsonSerializer.Deserialize<MazeSaveDto>(json);
        if (dto is null)
        {
            return null;
        }

        var maze = new Maze
        {
            Width = dto.Width,
            Height = dto.Height,
            Seed = dto.Seed,
            Random = new Random(),
            LogMessages = dto.Messages.ToList(),
        };

        foreach (var cellDto in dto.Cells)
        {
            maze.Cells.Add(CreateCell(cellDto.Type, cellDto.X, cellDto.Y, maze));
        }

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
            MazeWhereIWasCreated = maze,
        };

        var controller = new MazeContoller();
        controller.LoadMaze(maze);
        return controller;
    }

    private string GetSavePath(string sessionId)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var safeName = string.Concat(sessionId.Where(c => !invalidChars.Contains(c)));
        if (string.IsNullOrWhiteSpace(safeName))
        {
            safeName = "default";
        }

        // Stored under App_Data (outside wwwroot) so save files with player progress
        // are never reachable via a static file URL.
        return Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data", "saves", $"{safeName}.json");
    }

    private static IBaseCell CreateCell(string type, int x, int y, Maze maze)
    {
        IBaseCell cell = type switch
        {
            nameof(Amongus) => new Amongus(maze.Random),
            nameof(Coin) => new Coin(),
            nameof(Crater) => new Crater(),
            nameof(Diamond) => new Diamond(maze.Random),
            nameof(Dirt) => new Dirt(),
            nameof(Flower) => new Flower(),
            nameof(Ground) => new Ground(),
            nameof(HealthPotion) => new HealthPotion(),
            nameof(Ice) => new Ice(),
            nameof(MimicChest) => new MimicChest(maze.Random),
            nameof(PaidDoor) => new PaidDoor(),
            nameof(PileOfSand) => new PileOfSand(),
            nameof(Portal) => new Portal(),
            nameof(Rainbow) => new Rainbow(),
            nameof(Snake) => new Snake(),
            nameof(Starbucks) => new Starbucks(),
            nameof(Thief) => new Thief(),
            nameof(Tree) => new Tree(),
            nameof(VodkaBar) => new VodkaBar(),
            nameof(Wall) => new Wall(),
            _ => throw new InvalidOperationException($"Unknown cell type '{type}' in save file"),
        };

        cell.X = x;
        cell.Y = y;
        cell.MazeWhereIWasCreated = maze;
        return cell;
    }
}
