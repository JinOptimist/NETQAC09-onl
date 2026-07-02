namespace MazeConsole.MazeModels.Cells;

public class IlyaCell : BaseCell
{
    public override char MySymbol => 'I';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("You found Ilya Cell!");

        var random = new Random();
        var coins = random.Next(1, 6);

        player.Coin += coins;

        Console.WriteLine($"You received {coins} coins.");
        Console.WriteLine($"Now you have {player.Coin} coins.");

        return true;
    }
}