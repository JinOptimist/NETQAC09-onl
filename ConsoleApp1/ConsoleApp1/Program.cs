Console.WriteLine("The game Guess the number");

Console.WriteLine("Enter MIN number");
var MIN_NUMBER = int.Parse(Console.ReadLine());

Console.WriteLine("Enter MAX number");
var MAX_NUMBER = int.Parse(Console.ReadLine());

var MAX_ATTEMPT = (int)Math.Ceiling(Math.Log2(MAX_NUMBER - MIN_NUMBER + 1));

Console.WriteLine("Who will choose magic number?");
Console.WriteLine("1 - Human");
Console.WriteLine("2 - Computer");

var mode = int.Parse(Console.ReadLine());

var userMagicNumber = 0;

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
else
{
    var random = new Random();
    userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
}

Console.Clear();

var attempt = 0;
int guess;
var isWin = false;

var currentMinNumber = MIN_NUMBER;
var currentMaxNumber = MAX_NUMBER;

do
{
    Console.WriteLine($"Current range: [{currentMinNumber} - {currentMaxNumber}]");
    Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt + 1} / {MAX_ATTEMPT}]");

    var guessText = Console.ReadLine();
    guess = int.Parse(guessText);

    if (guess < currentMinNumber || guess > currentMaxNumber)
    {
        Console.WriteLine("Number is outside current range. Attempt is not counted.");
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
        Console.WriteLine("Our number is less");
        currentMaxNumber = guess - 1;
    }
    else if (guess == userMagicNumber)
    {
        isWin = true;
    }
} while (!isWin && attempt < MAX_ATTEMPT);

if (isWin)
{
    Console.WriteLine("Right! Your are Win");
}
else
{
    Console.WriteLine("Loooose");
}