using System;
using System.IO;
using System.Linq;
using System.Threading;
using MazeConsole.MazeModels.Cells;

namespace MazeConsole.MazeModels.Cells;

public class VodkaBar : BaseCell
{
    public override char MySymbol => '⚗';

    private void LogAction(string message)
    {
        try
        {
            File.AppendAllText("vodka.log", $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
        }
    }

    public override bool PlayerStepInMe(IPlayer player)
    {
        LogAction("Игрок вошел в VodkaBar.");
        Console.WriteLine("Игрок в баре 🍻🍻🍻");
        Thread.Sleep(5000);
        player.Coin = 0;

        var grounds = MazeWhereIWasCreated.Cells.OfType<Ground>().ToList();

        if (grounds.Any())
        {
            var randomGround = grounds[new Random().Next(grounds.Count)];
            player.X = randomGround.X;
            player.Y = randomGround.Y;
            LogAction($"Игрок проснулся на земле в [{player.X}, {player.Y}].");
        }
        else
        {
            // Случай, если земли на карте вообще нет
            string errorMsg = "КРИТИЧЕСКАЯ ОШИБКА: Земля (Ground) не найдена на карте!";
            LogAction(errorMsg);
            throw new Exception(errorMsg); 
        }

        Console.WriteLine("Упс... Вы пропили весь кэш и очнулись утром непонятно где...");
        Thread.Sleep(3000);
        return false;
    }
}