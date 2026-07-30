using MazeConsole;
using MazeConsole.MazeModels;
using WebAppSmile.Models;

namespace WebAppSmile.Services;

public static class MazeStateMapper
{
    public static MazeGameStateDto ToDto(MazeContoller controller, bool isFailed = false, string? errorMessage = null)
    {
        var maze = controller.Maze;
        var player = maze.Player;

        var cells = maze.Cells
            .Select(cell => new MazeCellDto
            {
                X = cell.X,
                Y = cell.Y,
                Type = cell.GetType().Name,
                IsPlayer = cell.X == player.X && cell.Y == player.Y
            })
            .OrderBy(c => c.Y)
            .ThenBy(c => c.X)
            .ToList();

        // Player occupies a cell visually; keep underlying type but mark IsPlayer
        foreach (var cell in cells.Where(c => c.IsPlayer))
        {
            cell.Type = nameof(Player);
        }

        return new MazeGameStateDto
        {
            Width = maze.Width,
            Height = maze.Height,
            Seed = maze.Seed,
            IsAlive = controller.IsAlive,
            IsFailed = isFailed,
            ErrorMessage = errorMessage,
            Player = new PlayerStatusDto
            {
                X = player.X,
                Y = player.Y,
                Coin = player.Coin,
                CurrentHealth = player.CurrentHealth,
                MaxHealth = Player.MAX_HEALTH,
                HealthPotion = player.HealthPotion,
                Sand = player.Sand,
                Flowers = player.Flowers
            },
            Cells = cells,
            Messages = maze.LogMessages.ToList()
        };
    }
}
