namespace MazeConsole.MazeModels.Cells;

public class Coin : BaseCell
{
    public const int COINT_COUNT_INITIAL = 3;

    /// <summary>
    /// Сколько монет ещё можно подобрать с этой клетки.
    /// Публичное свойство нужно для сохранения/загрузки состояния в JSON-сейв.
    /// </summary>
    public int CoinCount { get; set; } = COINT_COUNT_INITIAL;

    public Coin()
    {
        var task = new Task(CoinGrow);
        task.Start();
    }

    private void CoinGrow()
    {
        for (int i = 0; i < 100; i++)
        {
            CoinCount++;
            Thread.Sleep(1000);
            // Console.WriteLine($"New coin cost is: {CoinCount}");
        }
    }

    public override char MySymbol => 'c';

    public override bool PlayerStepInMe(IPlayer player)
    {
        if (player.Coin < 0)
        {
            throw new Exception("Player can't be with our money");
        }

        player.Coin++;

        CoinCount--;

        if (CoinCount == 0)
        {
            MazeWhereIWasCreated.ReplaceCellToGround(this);
        }

        MazeWhereIWasCreated.LogMessages.Add("Hey it's a coin");

        return true;
    }
}
