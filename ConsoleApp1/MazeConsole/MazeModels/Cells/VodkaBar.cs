namespace MazeConsole.MazeModels.Cells;

/* VodkaBar - место, попав в которое игрок зависает на долгое время,
 а потом просыпается мордой в рандомной клетке земли и без денег
 */
public class VodkaBar : BaseCell
{
    public override char MySymbol => '⚗'; // иконка на карте

    public override bool PlayerStepInMe(IPlayer player)
    {
        Console.WriteLine("Игрок в баре 🍻🍻🍻"); //логируем
        Thread.Sleep(5000);
        player.Coin = 0;

        var grounds = MazeWhereIWasCreated.Cells.OfType<Ground>().ToList();
    
        if (grounds.Any())
        {
            var randomGround = grounds[new Random().Next(grounds.Count)];
        
            player.X = randomGround.X;
            player.Y = randomGround.Y;
        }
        
        Console.WriteLine("Упс... Вы пропили весь кэш и очнулись утром непонятно где...");
        Thread.Sleep(3000);
        return false; 
    }
}