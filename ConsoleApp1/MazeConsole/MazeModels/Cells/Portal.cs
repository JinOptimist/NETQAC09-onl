using MazeConsole.MazeExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells;

public class Portal : BaseCell
{
 
    public override char MySymbol => 'P';

    public override bool PlayerStepInMe(IPlayer player)
    {

        if (player.Coin == 0)
        {
         
            string errorLog = $"[КРИТИЧЕСКАЯ ОШИБКА]: Игрок попытался войти в Портал на координатах ({X}; {Y}), " +
                              $"но у него {player.Coin} монет. Для телепортации требуется минимум 1 монета!";

            throw new MazeBuildException(player.Coin, errorLog);
        }

        player.Coin = player.Coin - 1;

        return true;
    }
}