namespace MazeConsole.MazeModels.Cells;

public class Coin : BaseCell
{
    public const int COINT_COUNT_INITIAL = 3;

    private int _coinCount = COINT_COUNT_INITIAL;

    public Coin()
    {
        var task = new Task(CoinGrow);
        task.Start();
    }

    private void CoinGrow()
    {
        for (int i = 0; i < 100; i++)
        {
            _coinCount++;
            Thread.Sleep(1000);
            // Console.WriteLine($"New coin cost is: {_coinCount}");
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

        _coinCount--;

        if (_coinCount == 0)
        {
            MazeWhereIWasCreated.ReplaceCellToGround(this);
        }

        MazeWhereIWasCreated.LogMessages.Add("Hey it's a coin");

        return true;
    }
}
