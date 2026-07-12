namespace MazeConsole.MazeModels.Cells;

public class Snake : BaseCell
{
    public override char MySymbol => 'S';

    private Random _random = new Random();
  
    public override bool PlayerStepInMe(IPlayer player)
    {
        player.SnakeMeets++;
        MoveSnake(player.SnakeMeets, _random);
        return true;
    }
           

    public void MoveSnake(int snakeMeets, Random random)
    {
        var oldNest = MazeWhereIWasCreated.Cells.First(x => x is Snake);
        MazeWhereIWasCreated.ReplaceCellToGround(oldNest);
        List<BaseCell> newNest = new List<BaseCell>();
        var randomCell = 0;

        
        newNest = MazeWhereIWasCreated.Cells.Where(x => x is Ground && !(x.X == oldNest.X && x.Y == oldNest.Y)).ToList();
        randomCell = random.Next(newNest.Count);
        
        if (newNest.Count==0)
        {
            throw new Exception("Not enough places for a nest!");
        }
        
        if (snakeMeets < 3 && newNest.Count > 0)
        {
        MazeWhereIWasCreated.ReplaceCellToSnake(newNest[randomCell]);
        MazeWhereIWasCreated.LogMessages.Add("You've scared the snake! And she has scared you!");
        }
        else
        {
        MazeWhereIWasCreated.LogMessages.Add("This is a bad neighborhood. Snake is moving out for good.");
        }
    }
}
