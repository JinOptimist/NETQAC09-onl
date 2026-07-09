public class GameResultPrinter
{
    public void PrintResult(bool isWin)
    {
        if (isWin)
        {
            Console.WriteLine("Right! You Win");
        }
        else
        {
            Console.WriteLine("Loooose");
        }
    }
}