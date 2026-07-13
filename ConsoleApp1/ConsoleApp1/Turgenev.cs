var game = new GameTurgenev();
game.Start();

class GameTurgenev
{
    private int MIN_NUMBER;
    private int MAX_NUMBER;
    private int MAX_ATTEMPT;

    private int userMagicNumber;

    private int currentMinNumber;
    private int currentMaxNumber;

    private int attempt;
    private bool isWin;

    public void Start()
    {
        Console.WriteLine("The game Guess the number");

        ChooseRange();

        MAX_ATTEMPT = (int)Math.Ceiling(Math.Log2(MAX_NUMBER - MIN_NUMBER + 1));

        ChooseMagicNumber();

        Console.Clear();

        Play();

        ShowResult();
    }

    private void ChooseRange()
    {
        Console.WriteLine("Enter MIN number");
        MIN_NUMBER = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter MAX number");
        MAX_NUMBER = int.Parse(Console.ReadLine());
    }

    private void ChooseMagicNumber()
    {
        Console.WriteLine("Who will choose magic number?");
        Console.WriteLine("1 - Human");
        Console.WriteLine("2 - Computer");

        var mode = int.Parse(Console.ReadLine());

        if (mode == 1)
        {
            bool isNumber;
            do
            {
                Console.WriteLine("User 1. Enter Magic number");

                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > MAX_NUMBER)
                {
                    Console.WriteLine($"Too big number. Must be lesser then {MAX_NUMBER}");
                }
                else if (userMagicNumber < MIN_NUMBER)
                {
                    Console.WriteLine($"Too small number. Must be bigger then {MIN_NUMBER}");
                }
            } while (!isNumber
                || userMagicNumber < MIN_NUMBER
                || userMagicNumber > MAX_NUMBER);
        }
        else
        {
            var random = new Random();
            userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
        }
    }

    private void Play()
    {
        attempt = 0;
        isWin = false;

        currentMinNumber = MIN_NUMBER;
        currentMaxNumber = MAX_NUMBER;

        int guess;

        do
        {
            Console.WriteLine($"Current range: [{currentMinNumber} - {currentMaxNumber}]");
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt + 1} / {MAX_ATTEMPT}]");

            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);

            if (guess < currentMinNumber || guess > currentMaxNumber)
            {
                Console.WriteLine("Number is outside of current range. An attempt is not counted.");
                continue;
            }

            attempt++;

            if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                currentMinNumber = guess + 1;
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is lesser");
                currentMaxNumber = guess - 1;
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < MAX_ATTEMPT);
    }

    private void ShowResult()
    {
        if (isWin)
        {
            Console.WriteLine("Right! Your have Won");
        }
        else
        {
            Console.WriteLine("Loooose");
        }
    }
}