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
            // 1. Откуда идет игрок
            var directionX = X - player.X;
            var directionY = Y - player.Y;

            // 2. куда игрок должен скользить (на 1 клетку дальше льда)
            var moveToX = X + directionX;
            var moveToY = Y + directionY;

            // 3. поиск ячейки , на которую игрок должен скользить
            var nextCell = MazeWhereIWasCreated.Cells
                .FirstOrDefault(cell => cell.X == moveToX && cell.Y == moveToY);

            // 4. Проверяем: если ячейка за льдом существует и наступабельная
            if (nextCell != null && nextCell.PlayerStepInMe(player))
            {
                // скользим
                player.X = moveToX;
                player.Y = moveToY;
            }
            else
            {
                // если ненаступабельная - останавливаемся на льду
                player.X = X;
                player.Y = Y;
            }

            //?
            return false;
        }
    }
}
