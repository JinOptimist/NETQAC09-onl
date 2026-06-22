class Savicka
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");

        int minRangeNumber;
        bool isMinRangeNumber;
        do
        {
            Console.WriteLine("Enter the minimum boundary of the range");

            var minRangeNumberText = Console.ReadLine();
            isMinRangeNumber = int.TryParse(minRangeNumberText, out minRangeNumber);

            if (!isMinRangeNumber)
            {
                Console.WriteLine("It's not a number");
            }
        } while (!isMinRangeNumber);

        int maxRangeNumber;
        bool isMaxRangeNumber;
        do
        {
            Console.WriteLine("Enter the maximum boundary of the range");

            var maxRangeNumberText = Console.ReadLine();
            isMaxRangeNumber = int.TryParse(maxRangeNumberText, out maxRangeNumber);

            if (!isMaxRangeNumber)
            {
                Console.WriteLine("It's not a number");

            }
            else if (maxRangeNumber<= minRangeNumber)
            {
                Console.WriteLine("The maximum boundary number has to be larger than the minimum boundary number");
            }
        } while (!isMaxRangeNumber || maxRangeNumber <= minRangeNumber);


        string playerSelectType;
        do
        {
            Console.WriteLine("Choose who defines a number. Press H if human and press C if computer");
            playerSelectType = Console.ReadLine();

            if (playerSelectType == "H")
            {
                Console.WriteLine("OK, the human will select a number to guess");
            }
            else if (playerSelectType == "C")
            {
                Console.WriteLine("OK, the computer will select a number to guess");
            }
            else 
            {
                Console.WriteLine("You can seletct only H or C");
            }
        } while (playerSelectType != "H" && playerSelectType != "C");

        
        var rangeSize = (maxRangeNumber - minRangeNumber) + 1;
        var  maxAttempt = (int)Math.Ceiling(Math.Log2(rangeSize));
        
        bool isNumber;
        int userMagicNumber;
        if (playerSelectType == "H")
        {
            do
            {
                Console.WriteLine($"User 1. Human, please enter a Magic number between {minRangeNumber} and {maxRangeNumber}");

                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > maxRangeNumber)
                {
                    Console.WriteLine($"Too big number. Must be less then {maxRangeNumber}");
                }
                else if (userMagicNumber < minRangeNumber)
                {
                    Console.WriteLine($"Too small number. Must be more then {minRangeNumber}");
                }
            } while (!isNumber
            || userMagicNumber < minRangeNumber
            || userMagicNumber > maxRangeNumber);
        }

        else
        {
            userMagicNumber = new Random().Next(minRangeNumber, maxRangeNumber+ 1);
            Console.WriteLine("The computer has successfully chosen a Magic number");
        }



Console.Clear();
        var attempt = 0;
        int guess;
        var isWin = false;
        var currentMinRangeNumber = minRangeNumber;
        var currentMaxRangeNumber = maxRangeNumber;
        
        
        do
        {
            attempt++;
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"User 2. Enter your guess. Attempt [{attempt} / {maxAttempt}]");
            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);

            if (guess < userMagicNumber)
            {
                currentMinRangeNumber = guess + 1;
                Console.WriteLine($"Our number is bigger. You current range is between {currentMinRangeNumber} and {currentMaxRangeNumber}");
            }
            else if (guess > userMagicNumber)
            {
                currentMaxRangeNumber = guess - 1;
                Console.WriteLine($"Our number is less. You current range is between {currentMinRangeNumber} and {currentMaxRangeNumber}");
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < maxAttempt);


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