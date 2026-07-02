using MazeConsole.MazeModels.Cells;

namespace MazeConsole.MazeModels;

public class Player : BaseCell
{
    public int Coin { get; set; }
    public int HealthPotion { get; set; }
    public int Armor { get; set; }

    public override char MySymbol => '@';

    public override bool PlayerStepInMe(Player player)
    {
        throw new NotImplementedException();
    }
}