namespace MazeConsole.MazeModels.Cells;

public class Snake : BaseCell
{
    public override char MySymbol => 'S';

    public override bool PlayerStepInMe(Player player)
    {
        // TODO: Player is scared of snakes, makes two steps in any direction
        return true;
    }
}
