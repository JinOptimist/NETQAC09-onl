namespace MazeConsole.MazeModels.Cells;

public class MimicChest : BaseCell
{
    private readonly Random _random;
    private readonly FileLogger _logger = new FileLogger();
    private const int FarmingVisitsThreshold = 10;
    private const int FarmingRepeatLogInterval = 5;

    // Сколько раз игрок заходил в этот сундук (для анти-фарма).
    // Публичное — чтобы сохранить/восстановить значение в JSON-сейве.
    public int VisitCount { get; set; }
    public override char MySymbol => 'M';
    public MimicChest(Random randomInput)
    {
        _random = randomInput;
    }

    public override bool PlayerStepInMe(IPlayer player)
    {
        VisitCount++;
        LogFarmingIfNeeded(player);
        MazeWhereIWasCreated.LogMessages.Add("Oh look it's a chest, surely there will be a lot of treasure, right?");

        // 0 - монетка, 1 - мимик кусает игрока
        int result = _random.Next(2);

        switch (result)
        {
            case 0:
                player.Coin++;
                MazeWhereIWasCreated.LogMessages.Add("You got a coin!");
                break;

            case 1:
                player.CurrentHealth--;
                MazeWhereIWasCreated.LogMessages.Add("Unlucky, its actually a mimic! It's dark and scary! And It bites you!");
                MazeWhereIWasCreated.LogMessages.Add($"Your health: {player.CurrentHealth}");
                break;
        }

        return true;
    }
    //Пока что логика такая, что в сундк можно заглядывать бесконечно, и либо находить монетку, либо терять здоровье, у нас тут казино!

    //HW6  Логируем подозрение на "фарм" сундука: если игрок посетил именно эту клетку
    private void LogFarmingIfNeeded(IPlayer player)
    {
        if (VisitCount < FarmingVisitsThreshold)
        {
            return;
        }

        var visitsOverThreshold = VisitCount - FarmingVisitsThreshold;
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
            $"Total visits this game: {VisitCount}"
        });
    }
    }