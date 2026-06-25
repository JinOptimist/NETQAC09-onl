using ConsoleApp1;
using ConsoleApp1.Atyletskaya;

class GameAtyletskaya
{
    public void Do()
    {

        var userMagicNumber = 0;
        bool isMagicNumberCorrect;

        Console.WriteLine("The game Guess the number v1");

        //выбор мин числа
        var minNumber = 1;
        bool isMinNumberCorrect;

        do
        {
            Console.WriteLine("Select min number");
            var userInputMinNumber = Console.ReadLine();
            isMinNumberCorrect = int.TryParse(userInputMinNumber, out minNumber);

            if (!isMinNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
                continue;
            }
            else if (minNumber < 0)
            {
                Console.WriteLine("Min number must be 0 or greater");
            }

        }
        while (!isMinNumberCorrect || minNumber < 0);


        //выбор макс числа
        var maxNumber = 100;
        bool isMaxNumberCorrect;

        do
        {
            Console.WriteLine("Select max number");
            var userInputMaxNumber = Console.ReadLine();
            isMaxNumberCorrect = int.TryParse(userInputMaxNumber, out maxNumber);

            if (!isMaxNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
            }
            else if (minNumber >= maxNumber)
            {
                Console.WriteLine($"Max number must be greater than {minNumber}");
            }
            else if (minNumber == maxNumber - 1)
            {
                Console.WriteLine("There's no point to do so");
            }

        }
        while (!isMaxNumberCorrect || minNumber >= maxNumber || minNumber == maxNumber - 1);

        //подсчет максимального количество попыток - засунули в класс GameAttemptsCalculator
        var calculator = new GameAttemptsCalculator();
        var maxAttempts = calculator.CalculateMaxAttempts(minNumber, maxNumber);

        Console.WriteLine($"For the range {minNumber} - {maxNumber} you'll get {maxAttempts} max attempts");

        //выбор режима игры - засунули в класс GameModeSelector
        var usersGameMode = new GameModeSelector();
        var selectedGameMode = usersGameMode.SelectGameMode();

        // выбор числа игроком - засунули в класс GamePlayerMagicNumberSelector
        if (selectedGameMode == 1)
        {
            var playersMagicNumber = new GamePlayerMagicNumberSelector();
            userMagicNumber = playersMagicNumber.SelectPlayerMagicNumber(minNumber, maxNumber);
        }

        // выбор рандомного числа - засунули в класс GameRNG
        else if (selectedGameMode == 2)
        {
            var rngNumber = new GameRng();
            userMagicNumber = rngNumber.GenerateRandomNumber(minNumber, maxNumber);
        }
        Console.Clear();

        //начало игры
        var updatedHint = new ConsoleApp1.Atyletskaya.GameHintsUpdater();
        updatedHint.NewMin = minNumber;
        updatedHint.NewMax = maxNumber;
        var attempt = 0;
        var isWin = false;

        do
        {
            Console.WriteLine($"Now guess the number. Attemmpt [{attempt} / {maxAttempts}]. Hint: number is between {updatedHint.NewMin} and {updatedHint.NewMax}");

            var guess = 0;
            bool isGuessCorrect;
            var guessText = Console.ReadLine();
            isGuessCorrect = int.TryParse(guessText, out guess);

            if (!isGuessCorrect)
            {
                Console.WriteLine("This is not a number.");
                continue;
            }

            else if (guess == userMagicNumber)
            {
                isWin = true;
            }

            else if (guess > maxNumber || guess < minNumber)
            {
                Console.WriteLine($"Reminder: you must guess number between {minNumber} and {maxNumber}");
                continue;
            }

            else if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                attempt++;
                // апдейт подсказки - положили в отдельный класс GameHintsUpdater
                updatedHint.CheckNewGuess(guess, userMagicNumber);
            }

            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                attempt++;
                // апдейт подсказки - положили в отдельный класс GameHintsUpdater
                updatedHint.CheckNewGuess(guess, userMagicNumber);
            }

        } while (!isWin && attempt < maxAttempts);


        if (isWin)
        {
            Console.WriteLine("Right! You won");
        }
        else
        {
            Console.WriteLine($"Good luck next time. The number was {userMagicNumber}");
        }
    }
}