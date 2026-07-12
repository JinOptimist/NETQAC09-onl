namespace MazeConsole.MazeModels.Cells;

public class PaidDoor : BaseCell
{
    private const int DOOR_PRICE = 2;

    public override char MySymbol => 'D';

    public override bool PlayerStepInMe(IPlayer player)
    {

        if (player.Coin < DOOR_PRICE)
        {
            MazeWhereIWasCreated.LogMessages.Add("You need 2 coins to open this door");

            //log
            throw new Exception(
                $"PaidDoor error. Position: {GetMyPosition()}." 
                +$"Need coins: {DOOR_PRICE}. Player coins: {player.Coin}." 
                +$"Maze seed: {MazeWhereIWasCreated.Seed}.");
        }

        player.Coin = player.Coin - DOOR_PRICE;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        MazeWhereIWasCreated.LogMessages.Add("You opened the paid door");
        return true;
    }
}
