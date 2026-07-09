namespace MazeConsole.MazeModels.Cells;

public class PaidDoor : BaseCell
{
    public override char MySymbol => 'D';

    public override bool PlayerStepInMe(Player player)
    {
        var doorPrice = 2;

        if (player.Coin < doorPrice)
        {
            MazeWhereIWasCreated.LogMessages.Add("You need 2 coins to open this door");

            //логируем
            throw new Exception(
                $"PaidDoor error. Position: {GetMyPosition()}. " +
                $"Need coins: {doorPrice}. Player coins: {player.Coin}. " +
                $"Maze seed: {MazeWhereIWasCreated.Seed}.");
        }

        player.Coin = player.Coin - doorPrice;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        MazeWhereIWasCreated.LogMessages.Add("You opened the paid door");
        return true;
    }
}
