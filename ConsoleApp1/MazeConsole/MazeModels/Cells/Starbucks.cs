namespace MazeConsole.MazeModels.Cells;

public class Starbucks : BaseCell
{
    public override char MySymbol => 'O';

    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Вы нашли Starbucks! Вы получаете бонус в виде Caramel Macchiato! Ваши силы увеличены!");
        return true;
    }
}