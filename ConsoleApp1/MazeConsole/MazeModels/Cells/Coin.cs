namespace MazeConsole.MazeModels.Cells;

public class Coin : BaseCell
{
    public override char MySymbol => 'c';

    public override bool PlayerStepInMe(Player player)
    {
        player.Coin++;
        return true;
    }
}

