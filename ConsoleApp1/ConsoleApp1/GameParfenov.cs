
using ConsoleApp1;

    class GameParfenov
{
    private int MAX_ATTEMPT = 10;
    private int MIN_NUMBER;
    private int MAX_NUMBER;
    private int userMagicNumber;
    private bool isWin;

    public void Play()
    {
        Console.WriteLine("The game Guess the number1");
        Console.WriteLine("set the range of natural numbers");

        SetRange();
        ChooseGuesser();
        PlayGuessing();
        ShowResult();
    }

    private void SetRange()
    {
        bool isNumberMin;
        do
        {
            Console.WriteLine("Please enter the MIN_NUMBER");
            var MIN_NUMBER_Text = Console.ReadLine();
            isNumberMin = int.TryParse(MIN_NUMBER_Text, out MIN_NUMBER);

            if (!isNumberMin)
                Console.WriteLine("It's not a number");
            else if (MIN_NUMBER < 0)
                Console.WriteLine("enter a number greater than 0");

        } while (!isNumberMin || MIN_NUMBER < 0);

        bool isNumberMax;
        var minMaxNumber = MIN_NUMBER + 1;
        do
        {
            Console.WriteLine($"Please enter the MAX_NUMBER > {minMaxNumber}");
            var MAX_NUMBER_Text = Console.ReadLine();
            isNumberMax = int.TryParse(MAX_NUMBER_Text, out MAX_NUMBER);

            if (!isNumberMax)
                Console.WriteLine("It's not a number");
            else if (MAX_NUMBER <= minMaxNumber)
                Console.WriteLine($"The number is too small. There must be more than {minMaxNumber}");

        } while (!isNumberMax || MAX_NUMBER <= minMaxNumber);
    }

    private void ChooseGuesser()
    {
        Console.WriteLine("will the user or randomizer guess the number?");
        bool isNumberFlow;
        var enterFlow = 0;

        do
        {
            Console.WriteLine("Please enter:\r\n1-user enters \r\n2-randomizer");
            var EnterFlowTexst = Console.ReadLine();
            isNumberFlow = int.TryParse(EnterFlowTexst, out enterFlow);

            if (enterFlow == 1)
            {
                bool isNumber;
                do
                {
                    Console.WriteLine("User 1. Enter Magic number");
                    var userMagicNumberText = Console.ReadLine();
                    isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                    if (!isNumber)
                        Console.WriteLine("It's not a number");
                    else if (userMagicNumber > MAX_NUMBER)
                        Console.WriteLine($"Too big number. Must be less then {MAX_NUMBER}");
                    else if (userMagicNumber < MIN_NUMBER)
                        Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");

                } while (!isNumber || userMagicNumber < MIN_NUMBER || userMagicNumber > MAX_NUMBER);
            }
            else if (enterFlow == 2)
            {
                var random = new Random();
                userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER);
            }

        } while (enterFlow != 1 && enterFlow != 2);
        Console.Clear();
    }

    private void PlayGuessing()
    {
        var attempt = 0;
        int guess;
        isWin = false;
        do
        {
            attempt++;
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);

            if (guess < userMagicNumber)
                Console.WriteLine("Our number is bigger");
            else if (guess > userMagicNumber)
                Console.WriteLine("Our number is less");
            else if (guess == userMagicNumber)
                isWin = true;

        } while (!isWin && attempt < MAX_ATTEMPT);
    }

    private void ShowResult()
    {
        if (isWin)
            Console.WriteLine("Right! Your are Win");
        else
            Console.WriteLine("Loooose");
    }
}