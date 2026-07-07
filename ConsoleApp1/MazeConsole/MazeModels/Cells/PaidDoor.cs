namespace MazeConsole.MazeModels.Cells;

public class PaidDoor : BaseCell
{
    public override char MySymbol => 'D';

    public override bool PlayerStepInMe(Player player)
    {
        var doorPrice = 2;

        if (player.Coin < doorPrice)
        {
            Console.WriteLine("You need 2 coins to open this door");
            return false;
        }

        player.Coin = player.Coin - doorPrice;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        Console.WriteLine("You opened the paid door");
        return true;
    }
}
