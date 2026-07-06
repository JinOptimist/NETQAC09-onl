namespace MazeConsole.MazeModels.Cells;


public class Flower : BaseCell
{
    public override char MySymbol => '*';

    public override bool PlayerStepInMe(Player player)
    {
        player.HealthPotion++;

        MazeWhereIWasCreated.ReplaceToCell(
            new Ground
            {
                X = X,
                Y = Y,
                MazeWhereIWasCreated = MazeWhereIWasCreated
            });

        return true;
    }
}

