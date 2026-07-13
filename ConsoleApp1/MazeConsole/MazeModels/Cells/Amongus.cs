namespace MazeConsole.MazeModels.Cells;

public class Amongus : BaseCell
{    public override char MySymbol => 'A';
    private Random random;
    public Amongus(Random randomInput)
    {
        random = randomInput;
    }
    public override bool PlayerStepInMe(IPlayer player)
    {
        if (player.CurrentHealth > Player.MAX_HEALTH || player.CurrentHealth < 0)
        {
            throw new NotImplementedException($"Player current health is {player.CurrentHealth} which is bigger then his Max Health or less then 0");
        }
        var damage = random.Next(1, 4);
        player.CurrentHealth = player.CurrentHealth - damage;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        return true;
    }
}
