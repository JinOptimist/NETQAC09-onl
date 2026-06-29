namespace MazeConsole.MazeModels.Cells;


public class Flower : BaseCell
{
    public override char MySymbol => '*';

    public override bool PlayerStepInMe(Player player)
    {
        return true;
    }
}

