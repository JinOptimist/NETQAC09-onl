namespace MazeConsole.MazeModels.Cells;

public class IlyaCell : BaseCell
{
    private static readonly Random Random = new();

    private const int MIN_COINS = 1;
    private const int MAX_COINS = 6;

    public override char MySymbol => 'I';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("You found Ilya Cell!");

        var coins = Random.Next(MIN_COINS, MAX_COINS);

        if (player.Coin < coins)
        {
            throw new Exception(
                $"IlyaCell error. Player position: {player.GetMyPosition()}. " +
                $"Cell position: {GetMyPosition()}. " +
                $"Player coins: {player.Coin}. Required coins: {coins}."
            );
        }

        player.Coin -= coins;

        Console.WriteLine($"Ilya Cell took {coins} coins.");
        Console.WriteLine($"Now you have {player.Coin} coins.");

        return true;
    }
}