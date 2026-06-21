class MHW
{
    public void Do()
    {

        int MAX_ATTEMPT = 0;
        int MAX_NUMBER = 0;
        int MIN_NUMBER = 1;
        var userMagicNumber = 0;
        bool isNumber;

        Console.WriteLine("Welcome to the Guess the number! Number one game of Wadiya!");
        (MAX_ATTEMPT, MAX_NUMBER) = DifficultyLevel();

        if (TypeChoice())
        {
            Console.WriteLine("Sure, let's go random");
            userMagicNumber = Random.Shared.Next(1, MAX_NUMBER);
        }
        else
        {
            do
            {
                Console.WriteLine("Please, enter The Magic number");

                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > MAX_NUMBER)
                {
                    Console.WriteLine($"Too big number. Must be less then {MAX_NUMBER}");
                }
                else if (userMagicNumber < MIN_NUMBER)
                {
                    Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");
                }
            } while (!isNumber
             || userMagicNumber < MIN_NUMBER
             || userMagicNumber > MAX_NUMBER);

        }
        ;

        Console.Clear();
        var attempt = 1;
        int guess;
        var isWin = false;


        do
        {
            Console.Clear();
            Console.WriteLine($"Our mystery number is between {MIN_NUMBER} and {MAX_NUMBER}");
            Console.WriteLine($"Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            var diffNumber = int.TryParse(guessText, out guess);

            if (!diffNumber)
            {
                Console.WriteLine($"Please, enter a valid value.");
            }
            else {
                if (guess > MAX_NUMBER)
                {
                    Console.WriteLine($"Oh, come on! I told you that {MAX_NUMBER} is a maximum. Try again.");
                }
                else if (guess < MIN_NUMBER)
                {
                    Console.WriteLine($"Nope, we can't go lower than {MIN_NUMBER}. Try again.");

                }
                else
                {
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
                }
            };
                
                

            
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


    bool TypeChoice()
    {
        bool randomizer = false;
        string playerInput;

        do
        {
            Console.WriteLine("Do you want a random number? Y or N");
            playerInput = Console.ReadLine();
            if (playerInput == "Y")
            {
                randomizer= true;

            } else if (playerInput == "N")
            {
                randomizer = false;
            } else
            {
                Console.WriteLine("It's only Y or N");
            }
        }
        while (playerInput != "Y" && playerInput != "N");


        return randomizer;
    }

    static (int, int) DifficultyLevel ()
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

            if (!diffNumber)
            {
                Console.WriteLine("Come on, just 1, 2, 3 or 4, it's not that hard!.");
                
            }
            else if (difficulty == 1)
            {
                Console.WriteLine("Easy it is.");
                int maxValue = 10;
                return (AttemptCount(maxValue), maxValue);
            }
            else if (difficulty == 2)
            {
                Console.WriteLine("Medium? Nice.");
                int maxValue = 100;
                return (AttemptCount(maxValue), maxValue);

            }
            else if (difficulty == 3)
            {
                Console.WriteLine("Go hard or go home!");
                int maxValue = 1000;
                return (AttemptCount(maxValue), maxValue);
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

                return (AttemptCount(personalNumber), personalNumber);
            }
        } while (difficulty != 1 && difficulty != 2 && difficulty != 3 && difficulty != 4);

        return (0, 0);
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
}

