using MazeConsole.MazeExceptions;

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

    // если у игрока нет ни одного зелья здоровья, то своего сдоровья ему не хватит добыть алмаз
    private const int MinimalHealthPotionToHandleDiamond = 1;

    public Diamond(Random randomInput)
    {
        _random = randomInput;
    }

    public override char MySymbol => 'd';

    public override bool PlayerStepInMe(Player player)
    {
        if (player.HealthPotion < MinimalHealthPotionToHandleDiamond)
        {
            // если зелья нет то пишем ошибку в лог
            var errorMessage =
                $"[FATAL] Игрок подобрал алмаз на позиции {GetMyPosition()}, не имея при себе ни одного зелья здоровья - " +
                $"своего здоровья не хватило для добычи алмаза. " +
                $"Состояние игрока: здоровье={player.CurrentHealth}, монет={player.Coin}, зелий={player.HealthPotion}";

            // выводим сообщение в консоль
            Console.WriteLine(errorMessage);

            // переиспользуем уже существующий MazeBuildException -
            throw new MazeBuildException(MazeWhereIWasCreated.Seed, errorMessage);
            
        }

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
                
        // выводим сообщение в консоль
        MazeWhereIWasCreated.LogMessages.Add($"Игрок нашёл алмаз {GetMyPosition()} и продал его за {bonusCoins} монет.");

        MazeWhereIWasCreated.ReplaceCellToGround(this);

        return true;
    }
}