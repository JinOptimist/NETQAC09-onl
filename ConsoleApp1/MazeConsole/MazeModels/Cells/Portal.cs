using System;
using System.Collections.Generic;
using System.Text;


namespace MazeConsole.MazeModels.Cells;

public class Portal : BaseCell
{
    // 1. Символ нашей ячейки
    public override char MySymbol => 'P';

    // 2. Метод, который сработает, когда игрок наступит на портал
    public override bool PlayerStepInMe(Player player)
    {
        Console.WriteLine("Вы наступили на Портал!");
        Console.WriteLine("Пространство закружилось");
        Console.WriteLine("Но вы устояли. Идем дальше!");

        return true;
    }
}