namespace MazeConsole.MazeModels.Cells;

public class Ground : BaseCell
{
    public override char MySymbol => '.';

    public override bool PlayerStepInMe(Player player)
    {
        return true;
    }
}
