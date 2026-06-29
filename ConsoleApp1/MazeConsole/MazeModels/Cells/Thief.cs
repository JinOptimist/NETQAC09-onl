namespace MazeConsole.MazeModels.Cells;

public class Thief : BaseCell
{
    public override char MySymbol => 'T';
    
    public override bool PlayerStepInMe(Player player)
    {
        if (player.Coin > 0)
        { 
            player.Coin = 0;
        }
        return true;
        
    }
}