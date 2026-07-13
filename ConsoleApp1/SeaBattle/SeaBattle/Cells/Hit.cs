namespace SeaBattleConsole.SeaBattleModels.Cells;

public class Hit : BaseCell
{
    public override char MySymbol => 'X';
    public CellType Type { get; set; }
    public HitType SecondType { get;  set; }

    public Hit()
    {
        Type = CellType.neutral;
        SecondType = HitType.NonValid;
    }
    public override HitType PlayerShootInMe(int x, int y, int playerNumber)
    {
        return SecondType;
    }
}