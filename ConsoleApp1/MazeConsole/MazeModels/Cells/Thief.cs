namespace MazeConsole.MazeModels.Cells;

public class Thief : BaseCell
{
    private bool _isThiefActivated = false;
    public override char MySymbol => 'T';
    
    public override bool PlayerStepInMe(IPlayer player)
    {
        if (player.Coin > 0)
        { 
            player.Coin = 0;
            _isThiefActivated = true;
        }
        
        if (_isThiefActivated)
        {
            MazeWhereIWasCreated.ReplaceCellToGround(this);
        }
        
        return true;
    }
}