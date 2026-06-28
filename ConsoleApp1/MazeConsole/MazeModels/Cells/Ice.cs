using System;
using System.Collections.Generic;
using System.Text;

namespace MazeConsole.MazeModels.Cells
{
    public class Ice : BaseCell
    {
        public override char MySymbol => '=';

        public override bool PlayerStepInMe(Player player)
        {
            // где игрок был до того, как встал на лед
            var previousX = player.X;
            var previousY = player.Y;

            // в каком направлении игрок двигается
            var directionX = X - previousX;
            var directionY = Y - previousY;

            // определить, куда надо проскользить
            var moveToX = directionX + X;
            var moveToY = directionY + Y;

           //найти эту ячейку, куда игрок будет скользить (спасибо гугл)
            var nextCell = MazeWhereIWasCreated.Cells.First(cell => cell.X == moveToX && cell.Y == moveToY); 

            //наступабельная ли найденная ячейка
            if (nextCell.PlayerStepInMe(player) == false) 
            {
                player.X = X;
                player.Y = Y;
            }
            
            else if (nextCell.PlayerStepInMe(player) == true)
            {
                player.X = moveToX;
                player.Y = moveToY;
            }
        
            return true;
        }
    }
}
