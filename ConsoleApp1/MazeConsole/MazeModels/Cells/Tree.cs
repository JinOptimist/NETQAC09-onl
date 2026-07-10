namespace MazeConsole.MazeModels.Cells;
using MazeConsole.MazeExceptions;

public class Tree : BaseCell
{
    public int Durability { get; set; } = 5;
    public override char MySymbol => 'W';
    public override bool PlayerStepInMe(IPlayer player)
    {

        var toGos = MazeWhereIWasCreated.Cells.OfType<Ground>().ToList();

        if (!toGos.Any())
        {
            throw new NotImplementedException();
        }
        else
        {
            var randomGround = toGos[new Random().Next(toGos.Count)];

            player.X = randomGround.X;
            player.Y = randomGround.Y;
            var logger = new FileLogger();
            logger.AddLog($"Игрок пытался залезть на дерево, но упал на координаты ({randomGround.X}; {randomGround.Y})");
        }

        return false;
    }
}

