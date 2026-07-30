namespace MazeConsole.MazeModels.Cells
{
    public class Crater : BaseCell

    {
        public override char MySymbol => 'O'; //перезаписываю на символ для типа Яма

        public override bool PlayerStepInMe(IPlayer player)
        {
            if (player.Coin > 0)
            {
                player.Coin--;
            }

            var moveX = X; // по X не меняем позицию игрока
            var moveY = Y + 1; // опускаем на ячейку вниз -> логика ямы

            var nextCell = MazeWhereIWasCreated.Cells
                .FirstOrDefault(cell => cell.X == moveX && cell.Y == moveY);

            if (nextCell != null && nextCell.PlayerStepInMe(player)) // проверка, что можем передвигать игрока из ямы
            {
                player.X = moveX;
                player.Y = moveY;
            }
            else
            {
                player.X = X;
                player.Y = Y;
            }

            return false;
        }
    }
}
