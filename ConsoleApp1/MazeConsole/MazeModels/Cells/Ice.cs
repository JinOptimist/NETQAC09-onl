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

           //найти эту ячейку, куда игрок будет скользить
            var nextCell = MazeWhereIWasCreated.
            Cells.
            First(cell => cell.X == moveToX && cell.Y == moveToY); 

            //логику ниже нужно будет перепридумать в зависимости от того, как будет развиваться игра дальше (синергия с другими ячейками и свойствами ячеек, проверить на выезд за пределы лабиринта...)
            //наступабельная ли найденная ячейка
            if (nextCell.PlayerStepInMe(player) == false) 
            //если нет - останавливаемся на нашей ячейке льда
            {
                player.X = X;
                player.Y = Y;
            }
            
            else if (nextCell.PlayerStepInMe(player) == true)
            //если да - проскальзываем на следующую ячейку
            {
                player.X = moveToX;
                player.Y = moveToY;
            }
        
            return true;
        }
    }
}
