using MazeConsole.MazeModels;
using MazeConsole.MazeModels.Cells;

public class MazeDrawer
{
    public void Draw(Maze maze)
    {
        Console.Clear();
        Console.WriteLine($"Player has {maze.Player.Coin} coins and {maze.Player.HealthPotion} Health potions");
        for (int y = 0; y < maze.Height; y++)
        {
            Console.WriteLine();

            for (int x = 0; x < maze.Width; x++)
            {
                //BaseCell cell;
                //if (maze.Player.X == x && maze.Player.Y == y)
                //{
                //    cell = maze.Player;
                //}
                //else
                //{
                //    cell = maze
                //        .Cells
                //        .First(cell => cell.X == x && cell.Y == y);
                //}

                var cell = maze.Player.X == x && maze.Player.Y == y
                    ? maze.Player
                    : maze
                        .Cells
                        .First(cell => cell.X == x && cell.Y == y);

                Console.Write(cell.MySymbol);
            }
        }


        //var x1 = 4;
        //var y1 = 7;

        //Cell answer;
        //var array = maze.Cells;
        //for (int i = 0; i < array.Count; i++)
        //{
        //    var cell = array[i];
        //    if (cell.X == x1 && cell.Y == y1)
        //    {
        //        answer = cell;
        //        break;
        //    }
        //}

        //foreach (var element in array)
        //{
        //    if (element.X == 4 && element.Y == 7)
        //    {
        //        answer = element;
        //    }
        //}


        //foreach (var element in array)
        //{
        //    if (element.MySymbol == '.')
        //    {
        //        answer = element;
        //    }
        //}

        //var myCoolCell = array.First(element => element.MySymbol == '.');
    }
}