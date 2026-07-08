namespace MazeConsole.MazeModels.Cells;

public class Snake : BaseCell
{
    public override char MySymbol => 'S';

    private Random _random = new Random();
  
    public override bool PlayerStepInMe(Player player)
    {
        player.SnakeMeets++;
        MoveSnake(player.SnakeMeets, _random);
        return true;
    }
           

    public void MoveSnake(int snakeMeets, Random random)
    {
        var oldNest = MazeWhereIWasCreated.Cells.First(x => x is Snake);
        MazeWhereIWasCreated.ReplaceCellToGround(oldNest);
        if (snakeMeets < 3)
        {
        Console.WriteLine("You've scared the snake! And she has scared you!");// Сейчас печатается и сразу стирается при отображении нового лабиринта. Сделать общий метод для вывода сообщений и выводить там
        var newNest = MazeWhereIWasCreated.Cells.Where(x => x is Ground && !(x.X == oldNest.X && x.Y == oldNest.Y)).ToList();
        var randomCell = random.Next(newNest.Count);
        MazeWhereIWasCreated.ReplaceCellToSnake(newNest[randomCell]);  
        }
        else
        {
        Console.WriteLine("This is a bad neighborhood. Snake is moving out for good.");
        }
    }
}
