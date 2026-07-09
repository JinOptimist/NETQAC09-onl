using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells
{
    public class PileOfSand : BaseCell
    {
        public override char MySymbol => '^';
        public override ConsoleColor CellColor => ConsoleColor.DarkYellow;

        public override bool PlayerStepInMe(Player player)
        {
            if (player.Sand < 1)
            {
                player.Sand++;
            }

            return true;
        }
    }
}
