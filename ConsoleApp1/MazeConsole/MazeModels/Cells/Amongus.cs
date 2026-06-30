namespace MazeConsole.MazeModels.Cells;

public class Amongus : BaseCell
{    public override char MySymbol => 'A';
     
    public override bool PlayerStepInMe(Player player)
    {
        var random = new Random();
        var damage = random.Next(1, 4);
        player.CurrentHealth = player.CurrentHealth - damage;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        return true;
    }
}
