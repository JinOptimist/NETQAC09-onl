using MazeConsole.MazeModels.Cells;

namespace MazeConsole.MazeModels;

public class Player : BaseCell
{
    public int Coin { get; set; }
    public int HealthPotion { get; set; }

    public int SnakeMeets { get; set; }

    public override char MySymbol => '@';

    public override bool PlayerStepInMe(Player player)
    {
        throw new NotImplementedException();
    }
}