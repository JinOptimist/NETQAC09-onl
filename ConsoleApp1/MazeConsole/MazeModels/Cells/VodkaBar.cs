namespace MazeConsole.MazeModels.Cells;

/*
      Игрок зашел в бар и завис там надолго - временно убираем его с поля (координаты -1), чтобы он визуально исчез
      Потом очнулся хрен знает где лицом в земле (рандом)
*/

public class VodkaBar : BaseCell
{
    // обозначение ('⚗' - это дистиллятор)
    public override char MySymbol => '⚗';

    public override bool PlayerStepInMe(Player player)
    { 
        int currentX = player.X;
        int currentY = player.Y;
        
        player.X = -1;
        player.Y = -1;

        // имитируем блэкаут — программа засыпает на 2 секунды
        Thread.Sleep(2000);

        // игрок просыпается мордой в земле, поэтому ищем ячейки с типом Ground
        var safeGroundCells = MazeWhereIWasCreated.Cells
            .Where(c => c.GetType().Name == "Ground") 
            .ToList();

        // если земля нашлась, выбираем случайную клетку и кладем туда бухого игрока
        if (safeGroundCells.Count > 0)
        {
            Random random = new Random();
            var randomBed = safeGroundCells[random.Next(safeGroundCells.Count)];

            player.X = randomBed.X;
            player.Y = randomBed.Y;
        }
        // Если земля не нашлась - значит игрок трезвеет в той точке, откуда пришел
        else
        {
            player.X = currentX;
            player.Y = currentY;
        }
        
        return true;
    }
}