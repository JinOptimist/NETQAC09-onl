namespace MazeConsole.MazeModels.Cells;

public class Tree : BaseCell
{
    public int Durability { get; set; } = 5;
    public override char MySymbol => 'W';

    public override bool PlayerStepInMe(IPlayer player)
    {
        return false;
    }
}

