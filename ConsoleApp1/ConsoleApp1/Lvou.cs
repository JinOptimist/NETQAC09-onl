class MykolaAndruk
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");

        var MAX_NUMBER = 100;
        var MIN_NUMBER =1;
        int MAX_ATTEMPT; 


        var userMagicNumber = 0;
        bool isNumber;

        //Выбор мода
        var gameModeNumber = 0;
        bool isGameModeNumber;

        //Виды модов
        var gameModeHuman = 1;
        var gameModeComputer = 2;

        //Для диапазона чисел
        bool isMinNumberForRange;
        bool isMaxNumberForRange;



        do
        {
            Console.WriteLine("Specify the range of numbers in which the game will take place.");
            Console.WriteLine($"Enter the minimum number:");
            var minNumberText = Console.ReadLine();
            isMinNumberForRange = int.TryParse(minNumberText, out MIN_NUMBER);

            if (!isMinNumberForRange)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (!isMinNumberForRange);

        do
        {
            Console.WriteLine($"Enter the maximum number:");
            var maxNumberText = Console.ReadLine();
            isMaxNumberForRange = int.TryParse(maxNumberText, out MAX_NUMBER);

            if (!isMaxNumberForRange)
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            else if (MAX_NUMBER <= MIN_NUMBER)
            {
                Console.WriteLine("Maximum number must be greater than minimum number.");
                isMaxNumberForRange = false;
            }
        } while (!isMaxNumberForRange);
        
        MAX_ATTEMPT = (int)Math.Ceiling(Math.Log2(MAX_NUMBER - MIN_NUMBER + 1));

        Console.WriteLine($"The game will take place in the range from {MIN_NUMBER} to {MAX_NUMBER}.");
        Console.WriteLine($"Maximum number of attempts: {MAX_ATTEMPT}");

        do
        {
            Console.WriteLine("Choose game mode:");
            Console.WriteLine($"{gameModeHuman} - Human sets the number");
            Console.WriteLine($"{gameModeComputer} - Computer sets the number");
            var gameModeText = Console.ReadLine();


            isGameModeNumber = int.TryParse(gameModeText, out gameModeNumber);
            if (!isGameModeNumber || gameModeNumber != 1 && gameModeNumber != 2)
            {
                Console.WriteLine("Invalid mod. Please try again.");
            }
        } while (!isGameModeNumber
        || (gameModeNumber != 1 && gameModeNumber != 2));

           if (gameModeNumber == gameModeHuman)
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
                } while (!isNumber || userMagicNumber < MIN_NUMBER || userMagicNumber > MAX_NUMBER);
            } else if (gameModeNumber == gameModeComputer)
            {
                var random = new Random();
                userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
                Console.WriteLine($"Computer has set the magic number between {MIN_NUMBER} and {MAX_NUMBER}");
            }

        Console.Clear();
        var attempt = 0;
        int guess;
        var isWin = false;
        do
        {
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}].Please enter a number between {MIN_NUMBER} and {MAX_NUMBER}.");
            var guessText = Console.ReadLine();
            if (!int.TryParse(guessText, out guess))
            {
                Console.WriteLine("It's not a number");
                continue;
            }
            if (guess < MIN_NUMBER || guess > MAX_NUMBER)
            {
                Console.WriteLine($"Number is out of range. Please enter a number between {MIN_NUMBER} and {MAX_NUMBER}.");
                continue;
            }

            attempt++;

            if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
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