// Запускает цикл угадывания и возвращает bool isWin для GameResultPrinte
public class GuessingProcessor
{
    public bool StartGuessing(GameRangeSettings rangeSettings, int secretNumber)
    {
        var currentMin = rangeSettings.MinNumber;
        var currentMax = rangeSettings.MaxNumber;
        var attempt = 0;
        var isWin = false;
        do
        {
            Console.WriteLine($"Current range is from {currentMin} to {currentMax}");
            
            var guessText = Console.ReadLine();
            var isGuessValid = int.TryParse(guessText, out var guess);
            if (!isGuessValid)
            {
                Console.WriteLine("It's not a number");
            }
            else if (guess < currentMin || guess > currentMax)

            {
                Console.WriteLine($"Wrong choice. Must be from {currentMin} to {currentMax}");
            }

            else

            {
                attempt++;
                if (guess < secretNumber)
                {
                    Console.WriteLine("Our number is bigger");
                    currentMin = guess + 1;
                }
                else if (guess > secretNumber)
                {
                    Console.WriteLine("Our number is less");
                    currentMax = guess - 1;
                }
                else
                {
                    isWin = true;
                }
            }
        } while (!isWin && attempt < rangeSettings.MaxAttempt);

        return isWin;
    }
}