namespace SeaBattleConsole.SeaBattleModels.Cells;

public class Miss : BaseCell
{
    public override char MySymbol => 'O';
    public CellType Type { get;  set; }
    public HitType SecondType { get; set; }

    public Miss()
    {
        Type = CellType.neutral;
        SecondType = HitType.NonValid;
    }
    public override HitType PlayerShootInMe(int x, int y, int playerNumber)
    {
        return SecondType;
    }
}