namespace MazeConsole.MazeModels.Cells;

public abstract class BaseCell
{
    public int X { get; set; }
    public int Y { get; set; }
    public Maze MazeWhereIWasCreated { get; set; }

    public abstract char MySymbol { get; }

    public abstract bool PlayerStepInMe(Player player);

    public string GetMyPosition()
    {
        return $"[{X}, {Y}]";
    }
}
