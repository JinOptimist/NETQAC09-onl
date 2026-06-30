namespace MazeConsole.MazeModels.Cells;

public class Coin : BaseCell
{
    private int coinCount = 3;
    public override char MySymbol => 'c';

    public override bool PlayerStepInMe(Player player)
    {
        player.Coin++;

        coinCount--;

        if (coinCount == 0)
        {
            MazeWhereIWasCreated.ReplaceCellToGround(this);
        }

        return true;
    }
}

