namespace MazeConsole.MazeModels.Cells;

public class MimicChest : BaseCell
{
    private readonly Random _random = new Random();
    public override char MySymbol => 'M';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Oh look it's a chest, surely there will be a lot of treasure, right?");
        // 0 или 1
        int result = _random.Next(2);

        if (result == 0)
        {
            player.Coin++;
            Console.WriteLine("You got a coin");
        }
       else
        {
            player.health--;
            Console.WriteLine("Unlucky, Its actually a mimic! It's dark and scary!");
        }
        return true;
    }
}
