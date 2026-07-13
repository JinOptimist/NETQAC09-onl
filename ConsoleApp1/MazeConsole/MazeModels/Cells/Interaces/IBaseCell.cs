using MazeConsole.MazeModels.Intefaces;

namespace MazeConsole.MazeModels.Cells.Interaces
{
    public interface IBaseCell
    {
        IMaze MazeWhereIWasCreated { get; set; }
        char MySymbol { get; }
        int X { get; set; }
        int Y { get; set; }

        string GetMyPosition();
        bool PlayerStepInMe(IPlayer player);
        string ToString();
    }
}