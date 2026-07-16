namespace SeaBattleConsole.SeaBattleModels.Cells;

public class ShipCell : BaseCell
{
    public override char MySymbol => 'S';
    public ShipCell(CellType playerType)
    {
        if (playerType != CellType.player1 && playerType != CellType.player2)
        {
            throw new ArgumentException("ShipCell requires player1 or player2");
        }

        Type = playerType;
        IsAvailable = false;
        SecondType = HitType.Hit;
    }
    public override HitType PlayerShootInMe(int x, int y, int playerNumber)
    {
        return SecondType;
    }
}
