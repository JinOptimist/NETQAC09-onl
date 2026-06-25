using System.ComponentModel.DataAnnotations;

/*class Gavrilenko
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
*/


class Gavrilenko
{
    public void Do()
    {
        Console.Clear();
        Console.WriteLine("The game 'Guess the number'"); //GameGavrilenko

        // Пользователь сам задает диапазон
        Console.Write("Enter MIN number: "); //InputService
        int MIN_INPUT_NUMBER = int.Parse(Console.ReadLine()!);

        Console.Write("Enter MAX number: ");
        int MAX_INPUT_NUMBER = int.Parse(Console.ReadLine()!);

        // Выбор режима игры
        Console.WriteLine("Who will guess the number?");//InputService
        Console.WriteLine("1 - User");
        Console.WriteLine("2 - Computer");

        int mode = int.Parse(Console.ReadLine()!);

        int userInputNumber = 0;

        // Если выбрали компьютер, он загадывает число
        if (mode == 2)
        {
            Random random = new Random(); //NumberRandom
            userInputNumber = random.Next(MIN_INPUT_NUMBER, MAX_INPUT_NUMBER + 1);
        }
        else
        {
            bool isNumber;

            Console.WriteLine(
                $"User. Enter number from {MIN_INPUT_NUMBER} to {MAX_INPUT_NUMBER}"//InputService
            );

            do
            {
                Console.WriteLine("User. Enter number");//InputService

                var userInputText = Console.ReadLine();
                isNumber = int.TryParse(userInputText, out userInputNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userInputNumber > MAX_INPUT_NUMBER)
                {
                    Console.WriteLine(
                        $"Too big number. Must be less then {MAX_INPUT_NUMBER}"
                    );
                }
                else if (userInputNumber < MIN_INPUT_NUMBER)
                {
                    Console.WriteLine(
                        $"Too small number. Must be more then {MIN_INPUT_NUMBER}"
                    );
                }//InputService

            } while (
                !isNumber
                || userInputNumber < MIN_INPUT_NUMBER 
                || userInputNumber > MAX_INPUT_NUMBER
            );
        }

        Console.Clear(); // Очистка консоли перед началом игры //GuessGame

        // Вычисление количества попыток и округление
        int MAX_ATTEMPT = (int)Math.Ceiling( //GuessGame
            Math.Log2(MAX_INPUT_NUMBER - MIN_INPUT_NUMBER + 1)
        );

        Console.WriteLine($"Attempts count: {MAX_ATTEMPT}");//GuessGame

        int attempt = 0;
        int guessNumber;
        bool isWin = false;

        // Текущий диапазон попыток
        int currentMin = MIN_INPUT_NUMBER;
        int currentMax = MAX_INPUT_NUMBER;

        do
        {
            // Показываем актуальный диапазон
            Console.WriteLine(
                $"Guess number from {currentMin} to {currentMax}"
            );

            Console.WriteLine(
                $"User. Enter your guessNumber. Attempt [{attempt} / {MAX_ATTEMPT}]"
            );

            var guessText = Console.ReadLine();

            while (!int.TryParse(guessText, out guessNumber))
            {
                Console.WriteLine("It's not a number");
                guessText = Console.ReadLine();
            }

            // Число вне диапазона НЕ считается попыткой
            if (
                guessNumber < currentMin
                || guessNumber > currentMax
            )
            {
                Console.WriteLine(
                    $"Number must be from {currentMin} to {currentMax}"
                );

                continue;
            }//GuessGame

            // Попытка считается только после проверки диапазона
            attempt++;//GuessGame

            if (guessNumber < userInputNumber)//GuessGame
            {
                Console.WriteLine("Our number is bigger");

                // Сужаем диапазон
                currentMin = guessNumber + 1;
            }
            else if (guessNumber > userInputNumber)
            {
                Console.WriteLine("Our number is less");

                // Сужаем диапазон
                currentMax = guessNumber - 1 ;
            }
            else
            {
                isWin = true;
            }

        } while (!isWin && attempt < MAX_ATTEMPT);

        if (isWin)
        {
            Console.WriteLine("Right! You are Win");
        }
        else
        {
            Console.WriteLine($"Loooose. Number was {userInputNumber}"); //GuessGame
        }
    }
}