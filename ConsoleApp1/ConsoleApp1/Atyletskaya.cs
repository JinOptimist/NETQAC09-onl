class Atyletskaya
{
    public void Do()
    {

        var userMagicNumber = 0;
        bool isMagicNumberCorrect;

        Console.WriteLine("The game Guess the number v1");

        //выбор мин числа
        var MIN_NUMBER = 1;
        bool isMinNumberCorrect;

        do
        {
            Console.WriteLine("Select min number");
            var userInputMinNumber = Console.ReadLine();
            isMinNumberCorrect = int.TryParse(userInputMinNumber, out MIN_NUMBER);

            if (!isMinNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
                continue;
            }
            else if (MIN_NUMBER < 0)
            {
                Console.WriteLine("Min number must be 0 or greater");
            }

        }
        while (!isMinNumberCorrect || MIN_NUMBER < 0);


        //выбор макс числа
        var MAX_NUMBER = 100;
        bool isMaxNumberCorrect;

        do
        {
            Console.WriteLine("Select max number");
            var userInputMaxNumber = Console.ReadLine();
            isMaxNumberCorrect = int.TryParse(userInputMaxNumber, out MAX_NUMBER);

            if (!isMaxNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
            }
            else if (MIN_NUMBER >= MAX_NUMBER)
            {
                Console.WriteLine($"Max number must be greater than {MIN_NUMBER}");
            }
            else if (MIN_NUMBER == MAX_NUMBER - 1)
            {
                Console.WriteLine("There's no point to do so");
            }

        }
        while (!isMaxNumberCorrect || MIN_NUMBER >= MAX_NUMBER || MIN_NUMBER == MAX_NUMBER - 1);

        //подсчет максимального количество попыток
        var minMaxRange = MAX_NUMBER - MIN_NUMBER;
        var MAX_ATTEMPT = 1;
        var attemptsCalc = minMaxRange;
        while (attemptsCalc > 1)
        {
            attemptsCalc = attemptsCalc / 2;
            MAX_ATTEMPT++;
        }
        Console.WriteLine($"For the range {MIN_NUMBER} - {MAX_NUMBER} you'll get {MAX_ATTEMPT} max attempts");

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
                else if (userMagicNumber > MAX_NUMBER)
                {
                    Console.WriteLine($"Can't be bigger than {MAX_NUMBER}");
                    continue;
                }
                else if (userMagicNumber < MIN_NUMBER)
                {
                    Console.WriteLine($"Can't be less than {MIN_NUMBER}");
                }
            }
            while (!isMagicNumberCorrect
    || userMagicNumber < MIN_NUMBER
    || userMagicNumber > MAX_NUMBER);
        }


        else if (selectedGameMode == 2)
        {
            var random = new Random();
            userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
        }
        Console.Clear();

        //начало игры
        var attempt = 0;
        int guess;
        var isWin = false;
        var closestMin = MIN_NUMBER;
        var closestMax = MAX_NUMBER;

        do
        {
            Console.WriteLine($"Now guess the number. Attemmpt [{attempt} / {MAX_ATTEMPT}]. Hint: number is between {closestMin} and {closestMax}");

            var guessText = Console.ReadLine();

            guess = int.Parse(guessText);

            if (guess > MAX_NUMBER || guess < MIN_NUMBER)
            {
                Console.WriteLine($"Reminder: you must guess number between {MIN_NUMBER} and {MAX_NUMBER}");
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
        } while (!isWin && attempt < MAX_ATTEMPT);


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