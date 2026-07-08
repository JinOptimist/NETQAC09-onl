using MazeConsole.MazeModels.Cells;

namespace MazeConsole.MazeModels;

// papa
public class Maze
{
    public int Seed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Random Random { get; set; }

    public Player Player { get; set; }

    public List<string> LogMessages { get; set; } = new List<string>();


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
        if (!Cells.Contains(oldCell))
        {
            throw new Exception($"There is no oldCell {oldCell}");
        }

        Cells.Remove(oldCell);
        var ground = new Ground
        {
            X = oldCell.X,
            Y = oldCell.Y,
            MazeWhereIWasCreated = oldCell.MazeWhereIWasCreated,
        };
        Cells.Add(ground);
    }

    public void ReplaceCellToTree(BaseCell oldCell)
    {
        Cells.Remove(oldCell);
        var tree = new Tree
        {
            X = oldCell.X,
            Y = oldCell.Y,
            MazeWhereIWasCreated = oldCell.MazeWhereIWasCreated,
        };
        Cells.Add(tree);
    }
}
