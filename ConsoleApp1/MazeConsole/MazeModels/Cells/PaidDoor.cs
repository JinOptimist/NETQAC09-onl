namespace MazeConsole.MazeModels.Cells;

public class PaidDoor : BaseCell
{
    public override char MySymbol => 'D';

    public override bool PlayerStepInMe(Player player)
    {
        var doorPrice = 2;

        if (player.Coin < doorPrice)
        {
            throw new Exception(
                $"PaidDoor error. Position: {GetMyPosition()}. " +
                $"Need coins: {doorPrice}. Player coins: {player.Coin}. " +
                $"Maze seed: {MazeWhereIWasCreated.Seed}.");
        }

        player.Coin = player.Coin - doorPrice;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        Console.WriteLine("You opened the paid door");
        return true;
    }
}
