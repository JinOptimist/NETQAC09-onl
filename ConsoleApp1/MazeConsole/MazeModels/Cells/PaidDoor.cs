namespace MazeConsole.MazeModels.Cells;

public class PaidDoor : BaseCell
{
    public override char MySymbol => 'D';

    public override bool PlayerStepInMe(Player player)
    {
        return true;
    }
}
