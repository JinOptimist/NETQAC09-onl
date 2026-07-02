using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells
{
    public class Dirt : BaseCell
    {
        public override char MySymbol => '~';

        public override bool PlayerStepInMe(Player player)
        {
            return true;
        }
    }
}