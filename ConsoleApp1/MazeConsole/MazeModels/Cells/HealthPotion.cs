namespace MazeConsole.MazeModels.Cells;

public class HealthPotion : BaseCell
{

    public override char MySymbol => '!';

    public override bool PlayerStepInMe(Player player)
    {
        player.HealthPotion++;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        return true;
    }
}
