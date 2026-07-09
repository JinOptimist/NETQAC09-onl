using MazeConsole.MazeModels.Cells;
using MazeConsole.MazeModels.Cells.Interaces;

namespace MazeConsole.MazeModels;

// papa
public class Maze : IMaze
{
    public int Seed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Random Random { get; set; }

    public IPlayer Player { get; set; }

    public List<string> LogMessages { get; set; } = new List<string>();


    // My child
    public List<IBaseCell> Cells { get; set; } = new();

    public void ReplaceToCell(IBaseCell newCell)
    {
        var oldCell = Cells.First(x => x.X == newCell.X
            && x.Y == newCell.Y);
        Cells.Remove(oldCell);
        Cells.Add(newCell);
    }

    public void ReplaceCellToGround(IBaseCell oldCell)
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

    public void ReplaceCellToTree(IBaseCell oldCell)
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

    public void ReplaceCellToSnake(IBaseCell oldCell)
    {
        Cells.Remove(oldCell);
        var snake = new Snake
        {
            X = oldCell.X,
            Y = oldCell.Y,
            MazeWhereIWasCreated = oldCell.MazeWhereIWasCreated,
        };
        Cells.Add(snake);
    }
}
