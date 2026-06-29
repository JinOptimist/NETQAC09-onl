namespace MazeConsole.MazeModels.Cells;

public class Amongus : BaseCell
{    public override char MySymbol => 'A';
     
    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Amongus! He stabs you for 1d4 before you strike back");
        var random = new Random();
        var damage = random.Next(1, 4);
        //TODO remove hp from player
        return true;
    }
}
