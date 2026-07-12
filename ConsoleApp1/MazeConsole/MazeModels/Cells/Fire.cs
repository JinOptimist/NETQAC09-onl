
using MazeConsole.MazeModels;
using MazeConsole.MazeExceptions;

using System;
namespace MazeConsole.MazeModels.Cells;
//огонь наносит урон игроку и не позволяет ему пройти в эту клетку, если здоровья <=0,пишет лог и генерирует ошибку.
public class Fire : BaseCell
{   private const int DAMAGE = 5;
    public override char MySymbol => 'F';
    public override bool PlayerStepInMe(Player player)
    {
        var healthBeforeFire = player.CurrentHealth;

        player.CurrentHealth -= DAMAGE;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"You stepped into fire. Damage: {DAMAGE}");
        Console.WriteLine($"Health before fire: {healthBeforeFire}");
        Console.WriteLine($"Current health: {player.CurrentHealth}");
        Console.ResetColor();

        if (player.CurrentHealth <= 0)
        {
            var errorMessage = // Log the error message
            $"Fire killed player. " +
            $"Fire position: {GetMyPosition()}, " +
            $"Damage: {DAMAGE}, "+ 
            $"Health before fire: {healthBeforeFire}, " +
            $"Health after fire: {player.CurrentHealth}, " + 
            $"Maze seed: {player.MazeWhereIWasCreated.Seed}";

            MazeWhereIWasCreated.LogMessages.Add(errorMessage);
            throw new FireCellException(errorMessage);

        }
        return false;
    }
}