namespace MazeConsole.MazeModels.Cells;

public class HealthPotion : BaseCell
{

    public override char MySymbol => '!';

    public override bool PlayerStepInMe(IPlayer player)
    {
        if (player.HealthPotion >= 1)
        {
            throw new InvalidOperationException($"Player already has max HealthPotions (1). Position: ({X}, {Y})");
        }

        player.HealthPotion++;
        MazeWhereIWasCreated.ReplaceCellToGround(this);

        var grounds = MazeWhereIWasCreated.Cells
            .OfType<Ground>()
            .ToList();

        if (!grounds.Any())
        {
            throw new InvalidOperationException("No ground type cells left to place Health Potion.");
        }
        
        var randomGround = grounds[MazeWhereIWasCreated.Random.Next(grounds.Count)];

        MazeWhereIWasCreated.ReplaceToCell(new HealthPotion
        {
            X = randomGround.X,
            Y = randomGround.Y,
            MazeWhereIWasCreated = MazeWhereIWasCreated
        });

        return true;
    }
}
