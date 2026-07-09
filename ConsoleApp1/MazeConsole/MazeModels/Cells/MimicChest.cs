namespace MazeConsole.MazeModels.Cells;

public class MimicChest : BaseCell
{
    private readonly Random _random = new Random();
    private readonly FileLogger _logger = new FileLogger();
    private const int FarmingVisitsThreshold = 10;
    private const int FarmingRepeatLogInterval = 5;
    private int _visitCount;
    public override char MySymbol => 'M';

    public override bool PlayerStepInMe(IPlayer player)
    {
        _visitCount++;
        LogFarmingIfNeeded(player);
        Console.WriteLine("Oh look it's a chest, surely there will be a lot of treasure, right?");

        // 0 - монетка, 1 - мимик кусает игрока
        int result = _random.Next(2);

        switch (result)
        {
            case 0:
                player.Coin++;
                Console.WriteLine("You got a coin!");
                break;

            case 1:
                player.CurrentHealth--;
                Console.WriteLine("Unlucky, its actually a mimic! It's dark and scary! And It bites you!");
                Console.WriteLine($"Your health: {player.CurrentHealth}");
                break;
        }

        return true;
    }
    //Пока что логика такая, что в сундк можно заглядывать бесконечно, и либо находить монетку, либо терять здоровье, у нас тут казино!

    //HW6  Логируем подозрение на "фарм" сундука: если игрок посетил именно эту клетку
    private void LogFarmingIfNeeded(IPlayer player)
    {
        if (_visitCount < FarmingVisitsThreshold)
        {
            return;
        }

        var visitsOverThreshold = _visitCount - FarmingVisitsThreshold;
        var isFirstWarning = visitsOverThreshold == 0;
        var isRepeatInterval = visitsOverThreshold % FarmingRepeatLogInterval == 0;

        if (isFirstWarning == false && isRepeatInterval == false)
        {
            return;
        }

        _logger.AddLog(new List<string>
        {
            "MimicChest farming suspected",
            $"Chest position: {GetMyPosition()}",
            $"Total visits this game: {_visitCount}"
        });
    }
    }