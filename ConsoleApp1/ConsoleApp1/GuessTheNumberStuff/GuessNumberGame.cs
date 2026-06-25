using ConsoleApp1.GuessTheNumberStuff;

class GuessNumberGame
{
    public void Play()
    {
        Console.WriteLine("The game Guess the number");

        var gameSettings = GenerateGameSettings();

        Console.Clear();
        var attempt = 0;
        var isWin = false;
        do
        {
            attempt++;
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            var guess = GetNumberFromConsole(
                $"User 2. Enter your guess. Attemmpt [{attempt} / {gameSettings.MaxAttempt}]");

            if (guess < gameSettings.MinValue)
            {
                Console.WriteLine("Our number is bigger");
            }
            else if (guess > gameSettings.MaxValue)
            {
                Console.WriteLine("Our number is less");
            }
            else if (guess == gameSettings.TheNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < gameSettings.MaxAttempt);


        if (isWin)
        {
            Console.WriteLine("Right! Your are Win");
        }
        else
        {
            Console.WriteLine("Loooose");
        }
    }

    private void ChooseGameMode()
    {
        var mode =
            GetNumberFromConsole("Enter 1, if play with bot. Enter 2 if flay with user", 1, 2);

        if (mode == 1)
        {

        }
        else if (mode == 2)
        {

        }
    }

    private GameSettings GenerateGameSettings()
    {
        ChooseGameMode();

        SetMinMaxValue();

        SetAttemptCount();

        return new GameSettings
        {
            MinValue = 0,
            MaxValue = 10,
            MaxAttempt = 3
        };
    }

    private void SetAttemptCount()
    {
        throw new NotImplementedException();
    }

    private void SetMinMaxValue()
    {
        throw new NotImplementedException();
    }

    private int GetNumberFromConsole(string messageForUser,
        int? minValue = null,
        int? maxValue = null)
    {
        int userMagicNumber;
        bool isNumber;
        do
        {
            Console.WriteLine(messageForUser);

            var userMagicNumberText = Console.ReadLine();
            isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (maxValue is null || userMagicNumber > maxValue)
            {
                Console.WriteLine($"Too big number. Must be less then {maxValue}");
            }
            else if (minValue is null || userMagicNumber < minValue)
            {
                Console.WriteLine($"Too small number. Must be more then {minValue}");
            }
        } while (!isNumber
                    || userMagicNumber < minValue
                    || userMagicNumber > maxValue);

        return userMagicNumber;
    }
}