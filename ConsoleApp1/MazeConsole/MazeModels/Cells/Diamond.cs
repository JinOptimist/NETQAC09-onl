namespace MazeConsole.MazeModels.Cells;

public class Diamond : BaseCell
{
    // Random передаётся снаружи, чтобы результат зависел от seed лабиринта
    private readonly Random _random;

    private const int BigBonusMin = 5;
    private const int BigBonusMax = 11;
    private const int SmallBonusMin = 3;
    private const int SmallBonusMax = 6;
    private const int MinimalBonus = 1;

    public Diamond(Random randomInput)
    {
        _random = randomInput;
    }

    public override char MySymbol => 'd';

    public override bool PlayerStepInMe(Player player)
    {
        int bonusCoins;

        if (player.Coin == 0)
        {
            bonusCoins = _random.Next(BigBonusMin, BigBonusMax);
        }
        else if (player.Coin < 3)
        {
            bonusCoins = _random.Next(SmallBonusMin, SmallBonusMax);
        }
        else
        {
            bonusCoins = MinimalBonus;
        }

        player.Coin += bonusCoins;

        Console.WriteLine($"Ты нашёл алмаз! Ты продал алмаз за {bonusCoins} монет!");

        MazeWhereIWasCreated.ReplaceCellToGround(this);

        return true;
    }
}

