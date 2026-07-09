using MazeConsole.MazeModels;

public class MazeDrawer
{
    private Maze _maze;
    public void Draw(Maze maze)
    {
        _maze = maze;
        Console.Clear();

        DrawMaze();
        
        Console.WriteLine($"\n\nPlayer has {maze.Player.Coin} coins and {maze.Player.HealthPotion} Health potions");

        foreach (var message in _maze.LogMessages)
        {
            Console.WriteLine(message);
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

    private void DrawMaze()
    {
        for (int y = 0; y < _maze.Height; y++)
        {
            Console.WriteLine();

            for (int x = 0; x < _maze.Width; x++)
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

                var cell = _maze.Player.X == x && _maze.Player.Y == y
                    ? _maze.Player
                    : _maze
                        .Cells
                        .First(cell => cell.X == x && cell.Y == y);

                var colorBefore = Console.ForegroundColor;
                Console.ForegroundColor = cell.CellColor;
                Console.Write(cell.MySymbol);
                Console.ForegroundColor = colorBefore;
            }
        }

    }
}