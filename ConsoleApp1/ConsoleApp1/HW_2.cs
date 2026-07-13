class HW_2
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");

        var maxNumber = 0;
        var minNumber = 0;
        var rangeChoice = 0;
        bool isRangeChoiceValid;
        var maxAttempt = 0;
        var gameModeChoice = 0;
        bool isGameModeChoiceValid;

        do
        {
            Console.WriteLine("Choose the range of numbers. 1. 1 - 100, 2. 101 - 500, 3. 501 - 1000");
            var rangeChoiceText = Console.ReadLine();
            isRangeChoiceValid = int.TryParse(rangeChoiceText, out rangeChoice);
            if (!isRangeChoiceValid)
            {
                Console.WriteLine("It's not a number");
            }
            else if (rangeChoice < 1 || rangeChoice > 3)
            {
                Console.WriteLine("Wrong choice. Must be 1, 2 or 3");
            }
            else if (rangeChoice == 1)
            {
                minNumber = 1;
                maxNumber = 100;
            }
            else if (rangeChoice == 2)
            {
                minNumber = 101;
                maxNumber = 500;
            }
            else if (rangeChoice == 3)
            {
                minNumber = 501;
                maxNumber = 1000;
            }
        } while (!isRangeChoiceValid || rangeChoice < 1 || rangeChoice > 3);
        var numbersCount = maxNumber - minNumber + 1;
        maxAttempt = (int)Math.Ceiling(Math.Log2(numbersCount));

        Console.WriteLine($"You choose range {minNumber} - {maxNumber}. Maximum attempts: {maxAttempt}");

        do
        {
            Console.WriteLine("Choose the game mode. 1. You create the secret number; 2. Computer creates the secret number");
            var gameModeChoiceText = Console.ReadLine();
            isGameModeChoiceValid = int.TryParse(gameModeChoiceText, out gameModeChoice);
            if (!isGameModeChoiceValid)
            {
                Console.WriteLine("It's not a number");
            }
            else if (gameModeChoice < 1 || gameModeChoice > 2)
            {
                Console.WriteLine("Wrong choice. Must be 1 or 2");
            }
            else if (gameModeChoice == 1)
            {
                Console.WriteLine("You choose to create the secret number");
            }
            else if (gameModeChoice == 2)
            {
                Console.WriteLine("You choose to let computer create the secret number");
            }
        }
        while (!isGameModeChoiceValid || gameModeChoice < 1 || gameModeChoice > 2);


        Console.WriteLine($"You choose game mode {gameModeChoice}");

        var secretNumber = 0;

        if (gameModeChoice == 1)
        {   
            bool isSecretNumberValid;
            do
            {
                Console.WriteLine($"Human creates the number. Please enter secret number from {minNumber} to {maxNumber}");
                var secretNumberText = Console.ReadLine();
                isSecretNumberValid = int.TryParse(secretNumberText, out secretNumber);
                if (!isSecretNumberValid)
                {
                    
                    Console.WriteLine("It's not a number");
                }
                else if (secretNumber < minNumber || secretNumber > maxNumber)
                {
                    Console.WriteLine($"Wrong choice. Must be from {minNumber} to {maxNumber}");
                }
            } while (!isSecretNumberValid || secretNumber < minNumber || secretNumber > maxNumber);
            Console.Clear();
        }

        else if (gameModeChoice == 2)
        {
            var random = new Random();
            secretNumber = random.Next(minNumber, maxNumber + 1);
            Console.WriteLine("Computer creates the number. Start guessing!");
        }

        var currentMin = minNumber;
        var currentMax = maxNumber;
        var attempt = 0;
        int guess;
        var isWin = false;

        do
        {
            Console.WriteLine($"Current range is from {currentMin} to {currentMax}");
            var guessText = Console.ReadLine();
            var isGuessValid = int.TryParse(guessText, out guess);
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
        }while (!isWin && attempt < maxAttempt);
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

    