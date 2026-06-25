namespace ConsoleApp1.GuessTheNumberStuff;

public class GameMakhortov
{
    public void Play()
    {
        Intro();
        var minBorder = MinRangeSetup();
        var maxBorder = MaxRangeSetup();
        var attempts = MaxAttemptsCount(maxBorder, minBorder);
        var riddler = Riddler();
        var magicNumber = MagicNumberSetup(riddler, maxBorder, minBorder);
        var isWin = Guesser(magicNumber, minBorder, maxBorder, attempts);
        GameState(isWin, magicNumber);
    }
    
    private void Intro()
    {
        Console.WriteLine($"The game Guess the number{Environment.NewLine}");
    }

    private int MinRangeSetup()
    {
        bool isValidInput;
        var minBorderValue = 0;
        do
        {
            Console.WriteLine("Enter the range which will be used for setting the Magic number up and provided to the Guesser:");
            Console.WriteLine("Enter the minimal border of a range");
            var minBorderString = Console.ReadLine();
            isValidInput = int.TryParse(minBorderString, out minBorderValue); 
        } while (!isValidInput);
        
        return minBorderValue;
    }

    private int MaxRangeSetup()
    {
        bool isValidInput;
        var maxBorderValue = 0;
        do
        {
            Console.WriteLine("Enter the maximal border of a range");
            var maxBorderString = Console.ReadLine();
            isValidInput = int.TryParse(maxBorderString, out maxBorderValue);
        } while (!isValidInput);
        
        return maxBorderValue;
    }

    private int MaxAttemptsCount(int maxBorderValue, int minBorderValue)
    {
        var attempts = (int)Math.Ceiling(Math.Log2(maxBorderValue - minBorderValue));
        Console.WriteLine($"Possible attempts count for guesser: {attempts}{Environment.NewLine}");
        
        return attempts;
    }

    private char Riddler()
    {
        
        Console.WriteLine("Who will set the Magic number up? Enter 'c' for computer or 'h' for human");
        char riddlerChar;
        
        bool isValidChoose = false;
        do
        {
            riddlerChar = Console.ReadKey().KeyChar; 
            switch (riddlerChar)
            {
                case 'c': 
                    Console.WriteLine($"You have chosen computer as a riddler");
                    isValidChoose = true;
                    break;
                case 'h': 
                    Console.WriteLine($"{Environment.NewLine}You have chosen human as a riddler{Environment.NewLine}");
                    isValidChoose = true;
                    break;
                default: 
                    Console.WriteLine("You have entered unsupported letter. Try one more time. Enter 'c' for computer or 'h' for human");
                    isValidChoose = false;
                    break;
            }
        } while (!isValidChoose);
        
        return riddlerChar;
    }

    private int MagicNumberSetup(char riddlerChar, int maxBorderValue, int minBorderValue)
    {
        var userMagicNumber = 0;
        if (riddlerChar == 'c')
        {
            userMagicNumber = new Random().Next(minBorderValue, maxBorderValue + 1);
        }
        else
        {
            bool isNumber;
            do
            {
                Console.WriteLine("Enter Magic number");
                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine($"It's not a number{Environment.NewLine}");
                }
                else if (userMagicNumber > maxBorderValue)
                {
                    Console.WriteLine($"Too big number. Must be less then {maxBorderValue}{Environment.NewLine}");
                }
                else if (userMagicNumber < minBorderValue)
                {
                    Console.WriteLine($"Too small number. Must be more then {minBorderValue}{Environment.NewLine}");
                }
            } while (!isNumber || userMagicNumber < minBorderValue || userMagicNumber > maxBorderValue);
        }
        Console.Clear();
        
        return userMagicNumber;
    }

    private bool Guesser(int userMagicNumber, int minBorderValue, int maxBorderValue, int attempts)
    {
        var attempt = 0;
        var isWin = false;
        do
        {
            attempt++;
            int guess;
            
            Console.WriteLine($"Enter your guess. Attempt [{attempt} / {attempts}]");

            bool isValidInput;
            do
            {
                var guessText = Console.ReadLine();
                isValidInput = int.TryParse(guessText, out guess);
                if (!isValidInput)
                {
                    Console.WriteLine("It's not a number, try again");
                }
            } while (!isValidInput);
            
            if (guess < userMagicNumber && guess > minBorderValue)
            {
                Console.WriteLine($"Our number is bigger. Try something from range: [{++guess};{maxBorderValue}]{Environment.NewLine}");
            }
            else if (guess > userMagicNumber && guess < maxBorderValue)
            {
                Console.WriteLine($"Our number is less. Try something from range: [{minBorderValue};{--guess}]{Environment.NewLine}");
            }
            else if (guess > maxBorderValue || guess < minBorderValue)
            {
                Console.WriteLine($"Your number is out of range. Try something from range: [{minBorderValue};{maxBorderValue}]{Environment.NewLine}");
                attempt--;
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < attempts);

        return isWin;
    }
    
    private void GameState(bool isWin, int userMagicNumber)
    {
        if (isWin)
        {
            Console.WriteLine("Right! Your are winner");
        }
        else
        {
            Console.WriteLine($"You have lost. The Magic number is {userMagicNumber}");
        }
    }
}