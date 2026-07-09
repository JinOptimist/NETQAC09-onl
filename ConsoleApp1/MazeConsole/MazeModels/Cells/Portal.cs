using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells;

public class Portal : BaseCell
{
    public override char MySymbol => 'P';

    public override bool PlayerStepInMe(IPlayer player)
    {
        return true;
    }
}