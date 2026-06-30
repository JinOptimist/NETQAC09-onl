using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells
{
    public class PileOfSand : BaseCell
    {
        public override char MySymbol => '^';

        public override bool PlayerStepInMe(Player player)
        {
            player.Sand++;
            return true;
        }
    }
}
