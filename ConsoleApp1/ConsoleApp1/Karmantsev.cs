using System.Numerics;

class Karmantsev
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");
        Console.WriteLine("Choose game mode:");
        Console.WriteLine("Type '1' - If you want to enter the number yourself");
        Console.WriteLine("Type '2' - The number will be chosen randomly");
        var gameMode = Console.ReadLine();
        int mode;
        var isNumberMode = int.TryParse(gameMode, out mode);
     


        //задаем рамки + кол-во попыток
        
        var MAX_NUMBER = 100;
        var MIN_NUMBER = 1;

       


        do
        {
            Console.WriteLine("Enter minimum number:");
            while (!int.TryParse(Console.ReadLine(), out MIN_NUMBER))
            {
                Console.WriteLine("It's not a number. Try again:");
            }

            Console.WriteLine("Enter maximum number:");
            while (!int.TryParse(Console.ReadLine(), out MAX_NUMBER))
            {
                Console.WriteLine("It's not a number. Try again:");
            }

            if (MIN_NUMBER >= MAX_NUMBER)
            {
                Console.WriteLine("Minimum number must be less than maximum number");
            }

        } 
        while (MIN_NUMBER >= MAX_NUMBER);

        //Вычислять максимальное количество попыток (с учётом того что игрок использует метод деления отрезка пополам)
        var countAttempt = MAX_NUMBER - MIN_NUMBER + 1;
        var MAX_ATTEMPT = (int)Math.Ceiling(Math.Log2(countAttempt));
        Console.WriteLine($"Maximum attempts: {MAX_ATTEMPT}");




        //загадываем число в зависимости от режима
        var userMagicNumber = 0;
        bool isNumber;

        if (mode == 1)
        {
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

            Console.Clear();
        }
        else if (mode == 2)
        {
            var random = new Random();
            userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);

            Console.WriteLine("Number generated randomly");
        }
        else
        {
            Console.WriteLine("Unknown mode");
            return;
        }


        //Выводить подсказки о диапазоне для игрока, после каждого хода
        var currentMin = MIN_NUMBER;
        var currentMax = MAX_NUMBER;






        //угадываем число + выводидим подсказки о диапазоне для игрока, после каждого хода
        var attempt = 0;
        int guess;
        var isWin = false;
        do
        {
            attempt++;
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"Please enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            //guess = int.Parse(guessText); в оригинале была эта строчка, но ее поменял на tryparse 
            var isGuessNumber = int.TryParse(guessText, out guess);

            if (!isGuessNumber)
            {
                Console.WriteLine("It's not a number");

                attempt--;
                continue;
            }

            //проверка на попадание введенного числа в диапазон
            if (guess < currentMin || guess > currentMax)
            {
                Console.WriteLine(
                    $"Incorrect guess, number must be in range {currentMin} - {currentMax}");

                attempt--;
                continue;
            }

            if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                currentMin = guess + 1;

                Console.WriteLine($"Number must be in range: {currentMin} - {currentMax}");
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                currentMax = guess - 1;

                Console.WriteLine($"Number must be in range: {currentMin} - {currentMax}");
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

        //Console.WriteLine("Hi I'm Pasha. I publish two books");
        //Console.WriteLine("Hello My Name is Kirilenko Iaroslav, I am QA");
        //Console.WriteLine("Hello My Name is Anna Tyletskaya, blah-blah-blah");
        //Console.WriteLine("Hi, I'm Andrei. I play drums");
        //Console.WriteLine("Hello My Name is Kirilenko Iaroslav, I am QA");
        //Console.WriteLine("Hello My Name is Jorjetta, I am from Armenia");
        //Console.WriteLine("Hello My Name is Viktoriya, I love raspberries and the smell of freshly cut grass");
        //Console.WriteLine("Hola! Yo soy Jack. Vivo en Montenegro");
        //Console.WriteLine("Hello My Name is Mykola Andruk, I have a pug");
        //Console.WriteLine("Hello My Name is Andrei, I love playing poker.");
        //Console.WriteLine("Hello My Name is A.Makhortov");
        //Console.WriteLine("Hello My Name is Max, I love snowboarding");
        //Console.WriteLine("Yo, my name's Andrey. I love swimming");
        //Console.WriteLine("Hi I'm Irina. I live in Prague");
        //Console.WriteLine("Hi I'm Irina2. I live in Prague");
        //Console.WriteLine("Hi, I'm Timur and i love to work every day");
        //Console.WriteLine("Hello My Name is Ekaterina, I love hiking");
        //Console.WriteLine("Dobar dan! My name is Valerii and this is some info about me.");


        //int age = 20;
        //char symbol = '1';
        //string name = "text";

        //bool isAdult = false;
        //bool condition1 = true;
        //bool condition2 = !isAdult; // not
        //bool condition3 = condition1 && condition2; // and
        //bool condition4 = condition3 || condition2; // or
        //bool condition5 = !condition2 && condition3 || condition4 && condition1;

        //var test = 123;
        //test = 1;
        //// test = "qwe";

        //var test2 = "Test";
        //// test2 = 123;
        //test2 = "qwe";

        //Console.WriteLine("Hi");
        //if (age > 60)
        //{
        //    Console.WriteLine("Go to vilage");
        //}
        //else if (age > 30)
        //{
        //    Console.WriteLine("Go to Work");
        //}
        //else if (age > 18 && condition2)
        //{
        //    if (condition4)
        //    {
        //        Console.WriteLine("Go to super univer");
        //    }
        //    Console.WriteLine("Go to univer 1");
        //}
        //else
        //{
        //    Console.WriteLine("Go to school 1");
        //    Console.WriteLine("Go to school 2");
        //}

        //Console.WriteLine("End");


        //var indexForNextWhile = 0;
        //while (indexForNextWhile < 10)
        //{
        //    indexForNextWhile = indexForNextWhile + 1;
        //    Console.WriteLine(indexForNextWhile);
        //}


    }
}