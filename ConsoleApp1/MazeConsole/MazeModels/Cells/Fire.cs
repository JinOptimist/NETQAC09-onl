namespace MazeConsole.MazeModels.Cells;
//огонь не пускает игрока
public class Fire : BaseCell
{ public override char MySymbol => 'F';
    public override bool PlayerStepInMe(Player player)
    {
        return true;
    }
}