using ConsoleApp1.GuessTheNumberStuff;

class GameTishkin
{
    //запуск игры
    public void Play()
    {
        Console.WriteLine("The game Guess the number");

        //настройка игры, спрашиваем диапазон ввода и magic number
        var gameSettings = GenerateGameSettings();

        Console.Clear();

        var attempt = 0;
        var isWin = false;

        //диапазон, меняется после попыток
        var currentMinValue = gameSettings.MinValue;
        var currentMaxValue = gameSettings.MaxValue;

        do
        {
            attempt++;

            var guess = GetNumberFromConsole(
                $"User 2. Enter your guess. Attempt [{attempt} / {gameSettings.MaxAttempt}]. Current range: [{currentMinValue} - {currentMaxValue}]",
                currentMinValue,
                currentMaxValue);

            if (guess < gameSettings.TheNumber)
            {
                currentMinValue = guess + 1;

                Console.WriteLine("Our number is bigger");
                Console.WriteLine($"Current range: [{currentMinValue} - {currentMaxValue}]");
            }
            else if (guess > gameSettings.TheNumber)
            {
                currentMaxValue = guess - 1;

                Console.WriteLine("Our number is less");
                Console.WriteLine($"Current range: [{currentMinValue} - {currentMaxValue}]");
            }
            else
            {
                isWin = true;
            }

        } while (!isWin && attempt < gameSettings.MaxAttempt);

        if (isWin)
        {
            Console.WriteLine("Right! You win!");
        }
        else
        {
            Console.WriteLine("Looooseeeeer!");
        }
    }

    //установка настроек игры
    private GameSettings GenerateGameSettings()
    {
        var minValue = GetNumberFromConsole("Enter min number:");
        var maxValue = GetNumberFromConsole("Enter max number:", minValue + 1);

        var theNumber = GetNumberFromConsole(
            "User 1. Enter magic number:",
            minValue,
            maxValue);

        var maxAttempt = CalculateMaxAttempt(minValue, maxValue);

        return new GameSettings
        {
            MinValue = minValue,
            MaxValue = maxValue,
            MaxAttempt = maxAttempt,
            TheNumber = theNumber
        }; 
    }

    //расчет попыток на основе диапазона
    private int CalculateMaxAttempt(int minValue, int maxValue)
    {
        var numbersCount = maxValue - minValue + 1;
        var maxAttempt = 0;

        while (numbersCount > 0)
        {
            maxAttempt++;
            numbersCount = numbersCount / 2;
        }

        return maxAttempt;
    }

    //проверка введенные данных
    private int GetNumberFromConsole(
        string messageForUser,
        int? minValue = null,
        int? maxValue = null)
    {
        int userNumber;
        bool isNumber;

        do
        {
            Console.WriteLine(messageForUser);

            var userNumberText = Console.ReadLine();
            isNumber = int.TryParse(userNumberText, out userNumber);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (maxValue is not null && userNumber > maxValue.Value)
            {
                Console.WriteLine($"Too big number. Must be less than or equal {maxValue.Value}");
            }
            else if (minValue is not null && userNumber < minValue.Value)
            {
                Console.WriteLine($"Too small number. Must be more than or equal {minValue.Value}");
            }

        } while (!isNumber
         || (maxValue is not null && userNumber > maxValue.Value)
         || (minValue is not null && userNumber < minValue.Value));

        return userNumber;
    }
}