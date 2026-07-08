namespace MazeConsole.MazeModels.Cells;

public class HealthPotion : BaseCell
{

    public override char MySymbol => '!';

    public override bool PlayerStepInMe(Player player)
    {
        if (player.HealthPotion >= 1)
        {
            throw new InvalidOperationException($"Player already has max HealthPotions (1). Position: ({X}, {Y})");
        }

        player.HealthPotion++;
        MazeWhereIWasCreated.ReplaceCellToGround(this);
        
        return true;
    }
}
