using MazeConsole.MazeExceptions;

namespace MazeConsole.MazeModels.Cells;

public class Flower : BaseCell
{
    public const int MAX_FLOWERS = 3;
    public override char MySymbol => '*';

    public override bool PlayerStepInMe(Player player)
    {
        CheckFlowersLimit(player);

        if (player.Flowers >= MAX_FLOWERS)
        {           
            MazeWhereIWasCreated.LogMessages.Add($"You found a flower, but your limit is reached ({MAX_FLOWERS}/{MAX_FLOWERS}).");
            return true;
        }

        player.Flowers++;
        MazeWhereIWasCreated.LogMessages.Add($"You found a flower! 🌸 {player.Flowers}/{MAX_FLOWERS}");

        MazeWhereIWasCreated.ReplaceCellToGround(this);

        return true;
    }

    private void CheckFlowersLimit(Player player)
    {
        if (player.Flowers < 0 || player.Flowers > MAX_FLOWERS)
        {
            throw new FlowerLimitException($"Cell Flower {GetMyPosition()}, LimitException: Player has {player.Flowers} flowers, but limit is {MAX_FLOWERS}");
        }
    }
}