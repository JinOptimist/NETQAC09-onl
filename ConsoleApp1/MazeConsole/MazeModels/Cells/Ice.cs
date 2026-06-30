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
            // если в инвентаре есть песок, то уменьшаем количество песка в инвентаре и меняем лед на грязь
            if (player.Sand > 0)
            {
                player.Sand--;

                var dirtCell = new Dirt { X = this.X, Y = this.Y };
                MazeWhereIWasCreated.Cells[MazeWhereIWasCreated.Cells.IndexOf(this)] = dirtCell;

                return true;
            }
                
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

            // уличная магия - запрещаем контроллеру двигать игрока, т.к. подвигали сами
            return false;
        }
    }
}
