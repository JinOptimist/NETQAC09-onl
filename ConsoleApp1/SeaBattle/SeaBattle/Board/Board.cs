using SeaBattleConsole.SeaBattleModels.Cells;
using System.Text.RegularExpressions;
namespace SeaBattleConsole.BoardTest;
public class Board
{
    public int Seed { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Random Random { get; set; }
    public List<BaseCell> Cells { get; set; } = new();

    public void ReplaceToCell(BaseCell newCell)
    {
        var oldCell = Cells.First(x => x.X == newCell.X
            && x.Y == newCell.Y);
        Cells.Remove(oldCell);
        Cells.Add(newCell);
    }

    public void ReplaceCellToWater(BaseCell oldCell)
    {
        if (!Cells.Contains(oldCell))
        {
            throw new Exception($"There is no oldCell {oldCell}");
        }

        Cells.Remove(oldCell);
        var water = new Water
        {
            X = oldCell.X,
            Y = oldCell.Y,
            BoardWhereIWasCreated = oldCell.BoardWhereIWasCreated,
        };
        Cells.Add(water);
    }
    public void ReplaceCellToMiss(BaseCell oldCell)
    {
        if (!Cells.Contains(oldCell))
        {
            throw new Exception($"There is no oldCell {oldCell}");
        }

        Cells.Remove(oldCell);
        var miss = new Miss
        {
            X = oldCell.X,
            Y = oldCell.Y,
            BoardWhereIWasCreated = oldCell.BoardWhereIWasCreated,
        };
        Cells.Add(miss);
    }
    public void ReplaceCellToHit(BaseCell oldCell)
    {
        if (!Cells.Contains(oldCell))
        {
            throw new Exception($"There is no oldCell {oldCell}");
        }

        Cells.Remove(oldCell);
        var hit= new Hit
        {
            X = oldCell.X,
            Y = oldCell.Y,
            BoardWhereIWasCreated = oldCell.BoardWhereIWasCreated,
        };
        Cells.Add(hit);
    }
    public ShipCell ReplaceCellToShip(BaseCell oldCell,CellType playerNumber)
    {
        if (!Cells.Contains(oldCell))
        {
            throw new Exception($"There is no oldCell {oldCell}");
        }

        Cells.Remove(oldCell);
        var ship = new ShipCell(playerNumber)
        {
            X = oldCell.X,
            Y = oldCell.Y,
            BoardWhereIWasCreated = oldCell.BoardWhereIWasCreated,
        };
        Cells.Add(ship);
        return ship;
    }
}
