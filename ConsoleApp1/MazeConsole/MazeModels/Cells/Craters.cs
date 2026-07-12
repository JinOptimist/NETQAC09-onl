namespace MazeConsole.MazeModels.Cells
{
    public class Crater : BaseCell

    {
        public override char MySymbol => 'O'; //перезаписываю на символ для типа Яма

        public override bool PlayerStepInMe(IPlayer player)
        {
            player.Y++;
            player.Coin--;
            return true;
        }
    }
}
