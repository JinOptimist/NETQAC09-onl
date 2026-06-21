using System.ComponentModel.DataAnnotations;

class Gavrilenko
{
    public void Do()
    {
        Console.Clear();
        Console.WriteLine("The game 'Guess the number'");

        var MAX_INPUT_NUMBER = 100;
        var MIN_INPUT_NUMBER = 1;

        var userInputNumber = 0;
        bool isNumber;
        
        Console.WriteLine($"User 1. Enter number from {MIN_INPUT_NUMBER} to {MAX_INPUT_NUMBER}");

       var MAX_ATTEMPT = (int)Math.Ceiling(Math.Log2(MAX_INPUT_NUMBER));
Console.WriteLine($"Attempts count: {MAX_ATTEMPT}");
        do
        {
            Console.WriteLine("User 1. Enter number");

            var userInputText = Console.ReadLine();
            isNumber = int.TryParse(userInputText, out userInputNumber);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (userInputNumber > MAX_INPUT_NUMBER)
            {
                Console.WriteLine($"Too big number. Must be less then {MAX_INPUT_NUMBER}");
            }
            else if (userInputNumber < MIN_INPUT_NUMBER)
            {
                Console.WriteLine($"Too small number. Must be more then {MIN_INPUT_NUMBER}");
            }
        } while (
            !isNumber
            || userInputNumber < MIN_INPUT_NUMBER
            || userInputNumber > MAX_INPUT_NUMBER
        );

        Console.Clear();
        var attempt = 0;
        int guessNumber;
        var isWin = false;

        do
        {
            attempt++; // attemp = attempt + 1;
            //Console.WriteLine("User 2. Enter your guessNumber. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"User 2. Enter your guessNumber. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            
            // TryParse - проверяет, можно ли преобразовать строку в число. 
            // Если да, то возвращает true и присваивает значение переменной guessNumber. Если нет, то возвращает false.
            while (!int.TryParse(guessText, out guessNumber))
            {
                Console.WriteLine("It's not a number");
                guessText = Console.ReadLine(); 
            }

            if (guessNumber < userInputNumber)//введенное число меньше загаданного
            {
                Console.WriteLine("Our number is bigger");
            }
            else if (guessNumber > userInputNumber)
            {
                Console.WriteLine("Our number is less");
            }
            else if (guessNumber == userInputNumber)
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
    }
}