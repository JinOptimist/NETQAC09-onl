class Atyletskaya
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

        //подсчет максимального количество попыток
        var minMaxRange = maxNumber - minNumber;
        var maxAttempts = 1;
        var attemptsCalc = minMaxRange;
        while (attemptsCalc > 1)
        {
            attemptsCalc = attemptsCalc / 2;
            maxAttempts++;
        }
        Console.WriteLine($"For the range {minNumber} - {maxNumber} you'll get {maxAttempts} max attempts");

        //выбор режима игры
        var selectedGameMode = 0;
        bool isGameModeCorrect;
        do
        {
            Console.WriteLine("Select game mode: Press 1 to play with a friend, Press 2 to enter training mode");
            var userInputGameMode = Console.ReadLine();
            isGameModeCorrect = int.TryParse(userInputGameMode, out selectedGameMode);

            if (!isGameModeCorrect)
            {
                Console.WriteLine("Please enter a number. Press 1 to play with a friend, Press 2 to enter training mode");
                continue;
            }
            else if (selectedGameMode != 1 && selectedGameMode != 2)
            {
                Console.WriteLine("We have 2 modes only :( Press 1 to play with a friend, Press 2 to enter training mode");
            }
        }
        while (!isGameModeCorrect || (selectedGameMode != 1 && selectedGameMode != 2));

        if (selectedGameMode == 1)
        {
            do
            {
                Console.WriteLine("Enter Magic number");

                var userMagicNumberText = Console.ReadLine();
                isMagicNumberCorrect = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isMagicNumberCorrect)
                {
                    Console.WriteLine("It's not a number");
                    continue;
                }
                else if (userMagicNumber > maxNumber)
                {
                    Console.WriteLine($"Can't be bigger than {maxNumber}");
                    continue;
                }
                else if (userMagicNumber < minNumber)
                {
                    Console.WriteLine($"Can't be less than {minNumber}");
                }
            }
            while (!isMagicNumberCorrect
    || userMagicNumber < minNumber
    || userMagicNumber > maxNumber);
        }


        else if (selectedGameMode == 2)
        {
            var random = new Random();
            userMagicNumber = random.Next(minNumber, maxNumber + 1);
        }
        Console.Clear();

        //начало игры
        var attempt = 0;
        int guess;
        var isWin = false;
        var closestMin = minNumber;
        var closestMax = maxNumber;

        do
        {
            Console.WriteLine($"Now guess the number. Attemmpt [{attempt} / {MAX_ATTEMPT}]. Hint: number is between {closestMin} and {closestMax}");

            var guessText = Console.ReadLine();

            guess = int.Parse(guessText);

            if (guess > maxNumber || guess < minNumber)
            {
                Console.WriteLine($"Reminder: you must guess number between {minNumber} and {maxNumber}");
                continue;
            }
            else if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                attempt++;
                // апдейт подсказки
                if (guess > closestMin)
                {
                    closestMin = guess;
                    continue;
                }
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                attempt++;
                //апдейт подсказки
                if (guess <= closestMax)
                {
                    closestMax = guess;
                }
            }

            else if (guess == userMagicNumber)
            {
                isWin = true;
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