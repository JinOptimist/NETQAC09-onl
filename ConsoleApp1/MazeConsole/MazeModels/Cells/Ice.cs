using MazeConsole.MazeExceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

                var dirtCell = new Dirt { X = X, Y = Y };
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

            //4.Проверяем: если ячейка за льдом существует и наступабельная
            var isNextCellSteppable = nextCell != null && nextCell.PlayerStepInMe(player);
            if (isNextCellSteppable)
            {
                // скользим
                player.X = moveToX;
                player.Y = moveToY;
            }
            else
            {
                //корректная логика - не скользим, остаемся на месте
                //player.X = X;
                //player.Y = Y;

                //сломанная логика - специально для HW6
                player.X = moveToX;
                player.Y = moveToY;
            }

            // проверка на случай, если игрока перенесло куда-то не туда
            if (player.X == moveToX && player.Y == moveToY && !isNextCellSteppable)
            {
                var iceSlideIssueType = nextCell == null ? "Out of map" : "Moved to unsteppable cell";
                var mazeSeed = MazeWhereIWasCreated.Seed;
                var IceSlideErrorLog = $" Player critically slided into ({moveToX}, {moveToY}). Issue: {iceSlideIssueType}. Maze seed: {mazeSeed}";

                MazeWhereIWasCreated.LogMessages.Add(IceSlideErrorLog);
                throw new IceCellExceptions(mazeSeed, moveToX, moveToY, IceSlideErrorLog);
            }

            // уличная магия - запрещаем контроллеру двигать игрока, т.к. подвигали сами
            return false;
        }
    }
}
