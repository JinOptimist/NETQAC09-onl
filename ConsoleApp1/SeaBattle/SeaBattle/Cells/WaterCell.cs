namespace SeaBattleConsole.SeaBattleModels.Cells;

public class Water: BaseCell 
{
    public override char MySymbol => '~';
    public Water()
    {
        Type = CellType.neutral;
        SecondType = HitType.Hit;
        IsAvailable = true;
    }
    public override HitType PlayerShootInMe(int x, int y,int playerNumber)
    {
        return SecondType;
    }
}
