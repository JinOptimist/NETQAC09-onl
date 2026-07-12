using MazeConsole.MazeModels.Cells.Interaces;

namespace MazeConsole.MazeModels;

public interface IPlayer : IBaseCell
{
    int Coin { get; set; }
    int CurrentHealth { get; set; }
    int HealthPotion { get; set; }
    char MySymbol { get; }
    int Sand { get; set; }
    int SnakeMeets { get; set; }
    int Flowers { get; set; }

    bool PlayerStepInMe(IPlayer player);
}
