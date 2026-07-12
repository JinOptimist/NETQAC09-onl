namespace MazeConsole.MazeModels.Cells;

public class Thief : BaseCell
{
    private bool _isThiefActivated = false;
    public override char MySymbol => 'T';
    public override bool PlayerStepInMe(IPlayer player)
    {
        try
        {
            if (player.Coin > 0)
            { 
                player.Coin = 0;
                _isThiefActivated = true;
                MazeWhereIWasCreated.LogMessages.Add($"(T) Thief: You have been robbed for {player.Coin} coins");
            }
            else if (player.Coin == 0)
            {
                MazeWhereIWasCreated.LogMessages.Add("(T) Thief: You are too poor");    
            }
            else
            {
                throw new Exception($"Player has invalid amount of coins to proceed. Player has {player.Coin} coins. Amount can not be less then 0. Exception is generated in Thief.cs");
            }
            
            if (_isThiefActivated)
            {
                MazeWhereIWasCreated.ReplaceCellToGround(this);
            }
            
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine($"Invalid amount of coins. Check logs for more information."); //force-stop exception
            throw;
        }
    }
}