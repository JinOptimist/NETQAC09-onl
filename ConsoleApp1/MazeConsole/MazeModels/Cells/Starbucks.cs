namespace MazeConsole.MazeModels.Cells;

public class Starbucks : BaseCell
{
    public override char MySymbol => 'O';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Вы нашли Starbucks! Ваш Caramel Macchiato придаёт сил!");

        var random = new Random();
        int coffeeBonus = random.Next(2, 6);
        player.Coin += coffeeBonus;

        Console.WriteLine($"Бонус: +{coffeeBonus} монет. Всего: {player.Coin}");

        if (player.Coin >= 15)
        {
            Console.WriteLine("Кофеиновая передозировка! Вы на пике сил.");
        }

        return true;
    }
}