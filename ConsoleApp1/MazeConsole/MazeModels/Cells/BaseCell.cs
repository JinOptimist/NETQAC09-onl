using MazeConsole.MazeModels.Cells.Interaces;

namespace MazeConsole.MazeModels.Cells;

public abstract class BaseCell : IBaseCell
{
    public int X { get; set; }
    public int Y { get; set; }
    public IMaze MazeWhereIWasCreated { get; set; }

    public abstract char MySymbol { get; }

    /// <summary>
    /// Return true if Player can move
    /// Also do some stuff with you
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public abstract bool PlayerStepInMe(IPlayer player);

    public string GetMyPosition()
    {
        return $"[{X}, {Y}]";
    }

    public override string ToString()
    {
        return $"[{X}, {Y}]";
    }
}
