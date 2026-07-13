using SeaBattleConsole.SeaBattleModels;
using SeaBattleConsole.BoardTest;
using SeaBattleConsole.SeaBattleModels.Cells;

public class BoardDrawer
{
    private Board _board;

    public void Draw(Board board, int PlayerNumber)
    {
        _board = board;

        DrawBoard(PlayerNumber);
    }

    private void DrawBoard(int playerNumber)
    {
        Console.WriteLine("  ABCDEFGHIJ");
        for (int y = 0; y < _board.Height; y++)
        {
            Console.WriteLine();
            Console.Write(y+" ");
            for (int x = 0; x < _board.Width; x++)
            {
                var cell = _board
        .Cells
        .First(cell => cell.X == x && cell.Y == y);
                if (cell.Type == CellType.player1 || cell.Type == CellType.player2)
                {
                    var water = new Water
                    {
                        X = x,
                        Y = y
                    };
                    Console.Write(water.MySymbol); 
                    // Console.Write(cell.MySymbol);   swap with this if you want to see ships
                }
                else
                {

                    Console.Write(cell.MySymbol);
                }

            }


        }
    }
}