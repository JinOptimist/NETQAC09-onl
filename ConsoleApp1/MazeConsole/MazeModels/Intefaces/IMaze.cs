using MazeConsole.MazeModels.Cells.Interaces;

namespace MazeConsole.MazeModels.Intefaces;

public interface IMaze
{
    List<IBaseCell> Cells { get; set; }
    int Height { get; set; }
    List<string> LogMessages { get; set; }
    IPlayer Player { get; set; }
    Random Random { get; set; }
    int Seed { get; set; }
    int Width { get; set; }

    void ReplaceCellToGround(IBaseCell oldCell);
    void ReplaceCellToSnake(IBaseCell oldCell);
    void ReplaceCellToTree(IBaseCell oldCell);
    void ReplaceToCell(IBaseCell newCell);
}