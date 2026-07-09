namespace MazeConsole.MazeModels.Cells;

public class Ground : BaseCell
{
    public override char MySymbol => '.';

    public override bool PlayerStepInMe(IPlayer player)
    {
        return true;
    }
}
