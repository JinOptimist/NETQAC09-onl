namespace MazeConsole.MazeModels.Cells;

public class MimicChest : BaseCell
{
    private readonly Random _random = new Random();
    public override char MySymbol => 'M';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Oh look it's a chest, surely there will be a lot of treasure, right?");

        // 0 - монетка, 1 - мимик кусает игрока, 2 - сундук оказался пустым
        int result = _random.Next(2);

        switch (result)
        {
            case 0:
                player.Coin++;
                Console.WriteLine("You got a coin!");
                break;

            case 1:
                player.Health--;
                Console.WriteLine("Unlucky, its actually a mimic! It's dark and scary! And It bites you!");
                Console.WriteLine($"Your health: {player.Health}/{player.MaxHealth}");
                break;
        }

        return true;
    }
    //Пока что логика такая, что в сундк можно заглядывать бесконечно, и либо находить монетку, либо терять здоровье, либо получать ничего, у нас тут казино!
}