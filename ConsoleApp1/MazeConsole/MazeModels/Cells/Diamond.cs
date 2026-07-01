namespace MazeConsole.MazeModels.Cells;

public class Diamond : BaseCell
{
    public override char MySymbol => 'd';

    public override bool PlayerStepInMe(Player player)
    {
              return true;
    }
}

