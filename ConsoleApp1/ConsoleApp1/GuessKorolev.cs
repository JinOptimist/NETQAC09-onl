namespace ConsoleApp1;
// собственно петля угадывания (чтоб не дублировать код)
public class GuessKorolev
{
    OnlyNumbersBro helper = new OnlyNumbersBro(); // проверяем 

    public void GuessLoop(int maxAttempts, int min, int max, int secretNumber, bool isBotMode)
    {
        var currentMin = min;
        var currentMax = max;
        var attempt = 0;
        var isWin = false;

        while (attempt < maxAttempts)
        {
            attempt++;

            if (isBotMode)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"-> Hint: The number is somewhere between [{currentMin} and {currentMax}]");
                Console.ResetColor();
            }

            var guess = helper.ReadNumber($"Attempt [{attempt} / {maxAttempts}]. Your guess: ");

            if (guess < secretNumber)
            {
                Console.WriteLine(isBotMode ? "HA-HA-HA! My number is BIGGER." : "Our number is bigger");
                if (guess >= currentMin) currentMin = guess + 1;
            }
            else if (guess > secretNumber)
            {
                Console.WriteLine(isBotMode ? "NOPE, BRO! My number is LESS." : "Our number is less");
                if (guess <= currentMax) currentMax = guess - 1;
            }
            else if (guess == secretNumber)
            {
                isWin = true;
                break;
            }
            Console.WriteLine();
        }

        if (isWin)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(isBotMode ? $"\nLucky bastard... You guessed it in {attempt} attempts! 😤" : "\nRight! PLAYER 2 WIN");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(isBotMode ? $"\nLOSER! You wasted all {maxAttempts} attempts. My number was {secretNumber}! 🖕" : "\nOhhh, PLAYER 2 LOOSE");
        }
        Console.ResetColor();
    }
}