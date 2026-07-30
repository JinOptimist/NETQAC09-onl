using MazeConsole.MazeModels.Cells;
using System.ComponentModel.DataAnnotations;

namespace MazeConsole.MazeModels;

public class Player : BaseCell, IPlayer
{
    public const int MAX_HEALTH = 20;
    public int Coin { get; set; }
    public int HealthPotion { get; set; }

    public int SnakeMeets { get; set; }

    public int CurrentHealth { get; set; } = MAX_HEALTH;
    public int Sand { get; set; }
    public override char MySymbol => '@';
    public int Flowers { get; set; } = 0;


    public override bool PlayerStepInMe(Player player)
    {
        throw new NotImplementedException();
    }
}