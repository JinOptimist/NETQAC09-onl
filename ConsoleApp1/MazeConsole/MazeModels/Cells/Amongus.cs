namespace MazeConsole.MazeModels.Cells;

public class Amongus : BaseCell
{    public override char MySymbol => 'A';
    private Random random;
    public Amongus(Random randomInput)
    {
        random = randomInput;
    }
    public override bool PlayerStepInMe(Player player)
    {
        var damage = random.Next(1, 4);
        player.CurrentHealth = player.CurrentHealth - damage;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        return true;
    }
}
