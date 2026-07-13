using SeaBattleConsole.BoardTest;
using SeaBattleConsole.SeaBattleModels.Cells;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SeaBattleConsole;
using SeaBattleConsole.SeaBattleModels;
public class SeaBattleContoller
{
    private Board boardPlayer1;
    private Board boardPlayer2;
    private int NumberOfShipsPlayerOne = 10;
    private int NumberOfShipsPlayerTwo = 10;

    public void Play()
    {
        var drawer = new BoardDrawer();
        var playerNumber = 1;
        var armies = BoardGeneration();
        var playerOneShips = armies.Item1;
        var playerTwoShips = armies.Item2;
        var currentBoardToDraw = boardPlayer1;
        var currentArmy = playerOneShips;
        while (NumberOfShipsPlayerOne > 0 && NumberOfShipsPlayerTwo > 0)
        {
            Console.Clear();
            if (playerNumber == 1)
            {
                currentBoardToDraw = boardPlayer2;
                currentArmy = playerTwoShips;

            }
            else
            {
                currentBoardToDraw = boardPlayer1;
                currentArmy = playerOneShips;
            }
            drawer.Draw(currentBoardToDraw,playerNumber);
            Console.WriteLine($"\nPlayer number {playerNumber} Please input coordinates in A0 Format from ABCDEFGHIJ and 0123456789");
            string input = Console.ReadLine();
            try 
            {
                if (input.Length != 2)
                {
                    InputError();
                    continue;
                }
                else if (char.IsLetter(input[0]) == false || char.IsDigit(input[1]) == false)
                {
                    InputError();
                    continue;
                }
                var x = char.ToUpper(input[0]) - 'A';
                var y = char.ToUpper(input[1]) - '0';
                if (y >= 10 || x >= 10 || x < 0 || y < 0)
                {
                    InputError();
                    continue;
                }
                var cell = currentBoardToDraw
                        .Cells
                        .First(cell => cell.X == x && cell.Y == y);
                var result = cell.PlayerShootInMe(x,y,playerNumber);
                if (result != HitType.NonValid)
                {

                    var waterTest = new Water
                    {
                        X = x,
                        Y = y
                    };
                    if (cell.MySymbol == waterTest.MySymbol)
                    {   
                        currentBoardToDraw.ReplaceCellToMiss(cell);
                        Console.WriteLine("MISS! Changing players");
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        currentBoardToDraw.ReplaceCellToHit(cell);
                        var shipHp = 0;
                        var found = false;
                        foreach (Ship ship in currentArmy)
                        {
                            foreach(ShipCell shipPart in ship.ShipParts)
                            {
                                if (shipPart.X == x && shipPart.Y == y)
                                {
                                    found = true;
                                    ship.NumberOfLives--;
                                    shipHp = ship.NumberOfLives;
                                    break;
                                }
                            }
                            if(found == true)
                            {
                                break;
                            }
                        }
                        if (shipHp == 0)
                        {
                            Console.WriteLine("DESTROYED! Do it again");
                            if (playerNumber == 1)
                            {
                                NumberOfShipsPlayerTwo--;
                            }
                            else
                            {
                                NumberOfShipsPlayerOne--;
                            }
                        }
                        else
                        {
                            Console.WriteLine("HIT! Do it again");
                        }
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }
                }
                else
                {
                    InputError();
                    continue;
                }
                if (playerNumber == 1)
                {
                    playerNumber = 2;
                }
                else
                {
                    playerNumber = 1;
                }
            }
            catch (Exception ex)
            {
                InputError();
                continue;
            }

        }
        Console.Clear();
        drawer.Draw(currentBoardToDraw, playerNumber);
        Console.WriteLine($"\nIt's over! Player number {playerNumber} won!");
    }

    private Tuple<List<Ship>, List<Ship>> BoardGeneration()
    {
        var builder = new BoardBuilder();
        // _board = builder.BuildTestBoard();
        var outcome = builder.BuildTestBoard();
        boardPlayer1 = outcome.Item1;
        boardPlayer2 = outcome.Item2;
        var resultTuple = Tuple.Create(outcome.Item3, outcome.Item4);
        return resultTuple;
    }
    private void InputError()
    {
        Console.WriteLine("Incorrect Input, please try again");
        System.Threading.Thread.Sleep(3000);
    }
}
