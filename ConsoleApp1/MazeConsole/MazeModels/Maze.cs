using MazeConsole.MazeModels.Cells;

namespace MazeConsole.MazeModels;

// papa
public class Maze
{
    public int Width { get; set; }
    public int Height { get; set; }
    

    public Player Player { get; set; }


    // My child
    public List<BaseCell> Cells { get; set; } = new();

    public void ReplaceToCell(BaseCell newCell)
    {
        var oldCell = Cells.First(x => x.X == newCell.X 
            && x.Y == newCell.Y);
        Cells.Remove(oldCell);
        Cells.Add(newCell);
    }

    public void ReplaceCellToGround(BaseCell oldCell)
    {
        Cells.Remove(oldCell);
        var ground = new Ground
        {
            X = oldCell.X,
            Y = oldCell.Y,
            MazeWhereIWasCreated = oldCell.MazeWhereIWasCreated,
        };
        Cells.Add(ground);
    }

}
