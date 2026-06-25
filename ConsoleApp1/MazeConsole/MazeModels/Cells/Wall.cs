namespace MazeConsole.MazeModels.Cells;

public class Wall : BaseCell
{
    public int Durability { get; set; } = 4;

    public override char MySymbol => '#';

    public override bool PlayerStepInMe(Player player)
    {
        return false;
    }
}
