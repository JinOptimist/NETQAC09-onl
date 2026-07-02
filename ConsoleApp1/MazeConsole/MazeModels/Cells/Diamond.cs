namespace MazeConsole.MazeModels.Cells;

public class Diamond : BaseCell
{
    public override char MySymbol => 'd';

    public override bool PlayerStepInMe(Player player)
    {
        var random = new Random();
                
        int bonusCoins;

        if (player.Coin == 0)
        {
            bonusCoins = random.Next(5, 11);
        }
        else if (player.Coin < 3)
        {
            bonusCoins = random.Next(3, 6);
        }
        else
        {
            bonusCoins = 1;
        }

        player.Coin = player.Coin + bonusCoins;

        Console.WriteLine($"Ты нашёл алмаз! Ты продал алмаз за {bonusCoins} монет!");
               
        MazeWhereIWasCreated.ReplaceCellToGround(this);

        return true;
    }
}

