
using ConsoleApp1;

    class GameParfenov
{
    private int _maxAttemp = 10;
    private int _minNumber;
    private int _maxNumber;
    private int _userMagicNumber;
    private bool _isWin;

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
            isNumberMin = int.TryParse(MIN_NUMBER_Text, out _minNumber);

            if (!isNumberMin)
            { 
                Console.WriteLine("It's not a number");
            }
            else if (_minNumber < 0)
            {
                Console.WriteLine("enter a number greater than 0");
            }

        } while (!isNumberMin || _minNumber < 0);

        bool isNumberMax;
        var minMaxNumber = _minNumber + 1;
        do
        {
            Console.WriteLine($"Please enter the MAX_NUMBER > {minMaxNumber}");
            var MAX_NUMBER_Text = Console.ReadLine();
            isNumberMax = int.TryParse(MAX_NUMBER_Text, out _maxNumber);

            if (!isNumberMax)
            {
                Console.WriteLine("It's not a number"); 
            }
            else if (_maxNumber <= minMaxNumber)
            {
                Console.WriteLine($"The number is too small. There must be more than {minMaxNumber}"); 
            }

        } while (!isNumberMax || _maxNumber <= minMaxNumber);
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
                    isNumber = int.TryParse(userMagicNumberText, out _userMagicNumber);

                    if (!isNumber)
                    { 
                        Console.WriteLine("It's not a number");
                    }
                        
                    else if (_userMagicNumber > _maxNumber)
                    {
                        Console.WriteLine($"Too big number. Must be less then {_maxNumber}"); 
                    }
                    else if (_userMagicNumber < _minNumber)
                    {
                        Console.WriteLine($"Too small number. Must be more then {_minNumber}"); 
                    }

                } while (!isNumber || _userMagicNumber < _minNumber || _userMagicNumber > _maxNumber);
            }
            else if (enterFlow == 2)
            {
                var random = new Random();
                _userMagicNumber = random.Next(_minNumber, _maxNumber);
            }

        } while (enterFlow != 1 && enterFlow != 2);
        Console.Clear();
    }

    private void PlayGuessing()
    {
        var attempt = 0;
        int guess;
        bool numberGuess;
        _isWin = false;
        do
        {
            attempt++;
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {_maxAttemp}]");
            var guessText = Console.ReadLine();
            numberGuess = int.TryParse(guessText, out guess);

            if (guess < _userMagicNumber)
            {
                Console.WriteLine("Our number is bigger"); 
            }
            else if (guess > _userMagicNumber)
            {
                Console.WriteLine("Our number is less"); 
            }
            else if (guess == _userMagicNumber)
            {
                _isWin = true; 
            }
            if (!numberGuess)
            {
                Console.WriteLine("It's not a number");
            }
        } while (!_isWin && attempt < _maxAttemp);
    }

    private void ShowResult()
    {
        if (_isWin)
        {
            Console.WriteLine("Right! Your are Win"); 
        }
        else
        {
            Console.WriteLine("Loooose"); 
        }
    }
}