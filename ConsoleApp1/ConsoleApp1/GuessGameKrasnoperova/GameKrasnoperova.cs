// игра "Угадай число"
namespace ConsoleApp1.GuessGameKrasnoperova;

class GameKrasnoperova
{
    public void Play()
    {
        Console.WriteLine("The game Guess the number");

        // задать диапазон чисел
        var minNumber = Random.Shared.Next(0, 501);
        var maxNumber = Random.Shared.Next(minNumber + 1, 511);

        // задать число для угадывания
        var theNumber = Random.Shared.Next(minNumber, maxNumber + 1);

        // определить количество попыток
        var range = maxNumber - minNumber + 1;
        var maxAttempts = (int)Math.Ceiling(Math.Log2(range));

        Console.Clear();
               
        // выбор режима игры
        var gameMode = new GameMode();
        var selectedGameMode = gameMode.SelectGameMode();

        if (selectedGameMode == 1)
        {
            Console.WriteLine("You selected to play with a people");
        }
        else if (selectedGameMode == 2)
        {
            Console.WriteLine("You selected to play with a bot");
        }

        Console.WriteLine("Let's start the game!");

        var attempt = 0;
        var isWin = false;
        
        // цикл игры

        do
        {
            attempt++;
            Console.WriteLine($"Player, enter your guess. Attemmpt [{attempt} / {maxAttempts}]. Number is between [{minNumber} and {maxNumber}]");

            var guess = Convert.ToInt32(Console.ReadLine());

            if (guess < minNumber)
            {
                Console.WriteLine("Our number is bigger");
            }
            else if (guess > maxNumber)
            {
                Console.WriteLine("Our number is less");
            }
            else if (guess > minNumber && guess < theNumber)
            {
                Console.WriteLine("Our number is bigger");
                minNumber = guess + 1;
            }
            else if (guess < maxNumber && guess > theNumber)
            {
                Console.WriteLine("Our number is less");
                maxNumber = guess - 1;
            }
            else if (guess == theNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < maxAttempts);


        if (isWin)
        {
            Console.WriteLine("Right! Your are Win");
        }
        else
        {
            Console.WriteLine("Loooose");
        }
    }
}