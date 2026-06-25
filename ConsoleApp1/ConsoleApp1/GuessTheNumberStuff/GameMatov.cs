class GameMatov

{
    public void Play()
    {

        var MAX_ATTEMPT = 0;
        var MAX_NUMBER = 0;
        var MIN_NUMBER = 1;
        var userMagicNumber = 0;

        Console.WriteLine("Welcome to the Guess the number! Number one game of Wadiya!");
        MAX_NUMBER = DifficultyLevel();
        MAX_ATTEMPT = AttemptCount(MAX_NUMBER);

        if (GetNumberFromConsole("Do you want a random number? 1 for Yes, 2 for No", 1, 2)==1)
            {
            Console.WriteLine("Sure, let's go random");
            userMagicNumber = Random.Shared.Next(1, MAX_NUMBER+1);
        }
        else
        {
            userMagicNumber = GetNumberFromConsole("Please, enter The Magic number", MIN_NUMBER, MAX_NUMBER);
            }
        ;

        Console.Clear();
        var attempt = 1;
        int guess;
        var isWin = false;


        do
        {
            Console.WriteLine($"Our mystery number is between {MIN_NUMBER} and {MAX_NUMBER}");
            guess = GetNumberFromConsole($"Enter your guess. Attempt [{attempt} / {MAX_ATTEMPT}]", MIN_NUMBER, MAX_NUMBER);
             attempt++;
            if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                MIN_NUMBER = guess;
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                MAX_NUMBER = guess;
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
            } 
        } while (!isWin && attempt <= MAX_ATTEMPT);


        if (isWin)
        {
            Console.WriteLine("Right! Your have won!");
        }
        else
        {
            Console.WriteLine("Tough luck, buddy. See you next time.");
        }


    }

    static int DifficultyLevel ()
    {
        var difficulty = 0;
        do
        {
            Console.WriteLine("Please, choose the difficulty level:");
            Console.WriteLine("1. Easy: 1-10");
            Console.WriteLine("2. Medium: 1-100");
            Console.WriteLine("3. Hard: 1-1000");
            Console.WriteLine("4. Personal limits");


            var difficultyInput = Console.ReadLine();
            var diffNumber = int.TryParse(difficultyInput, out difficulty);

            if (!diffNumber || difficulty > 4 || difficulty<1)
            {
                Console.WriteLine("Come on, just 1, 2, 3 or 4, it's not that hard!.");
                
            }
            else if (difficulty == 1)
            {
                Console.WriteLine("Easy it is.");
                int maxValue = 10;
                return maxValue;
            }
            else if (difficulty == 2)
            {
                Console.WriteLine("Medium? Nice.");
                int maxValue = 100;
                return maxValue;

            }
            else if (difficulty == 3)
            {
                Console.WriteLine("Go hard or go home!");
                int maxValue = 1000;
                return maxValue;
            }
            else if (difficulty == 4)
            {
                Console.WriteLine("Somethig personal? Ok, sure! Type a number from 2 to whatever you want!");
                var personalNumber = 2;
                var posibleNumber = false;
                do
                {
                    var personalInput = Console.ReadLine();
                    posibleNumber = int.TryParse(personalInput, out personalNumber);
                    if (!posibleNumber || personalNumber <2)
                    {
                    Console.WriteLine("No no no, we need a correct number.");
                    }
                } while (!posibleNumber || personalNumber < 2);

                return personalNumber;
            }
        } while (difficulty is not (>= 1 and <= 4));

        return 0;
    }

    static int AttemptCount (int MaxValue)
    {
        int attempt = 0;
        int possibleValue = 1;
        do {
            attempt++;
            possibleValue *= 2;
        }
        while (possibleValue <= MaxValue);

        return attempt;
    }

    static int GetNumberFromConsole(string messageForUser, int? minValue, int? maxValue)
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
                Console.WriteLine($"That's not a number. Please, type in range of {minValue} - {maxValue}");
            }
            else if (maxValue is null || userMagicNumber > maxValue)
            {
                Console.WriteLine($"Oh, come on! I told you that {maxValue} is a maximum. Try again.");
            }
            else if (minValue is null || userMagicNumber < minValue)
            {
                Console.WriteLine($"Nope, we can't go lower than {minValue}. Try again.");
            }
        } while (!isNumber
                    || userMagicNumber < minValue
                    || userMagicNumber > maxValue);

        return userMagicNumber;
    }
}

