class Drozd
{
    public void Do()
    {
               
        Console.WriteLine("The game Guess the number");

        var MAX_ATTEMPT = 0;
        var MIN_NUMBER = 0; 
        var MAX_NUMBER = 0;
        var userMagicNumber = 0;
        var opponentSelected = 1;
        bool isNumber;

        //выбор диапазона числа
        do
        {
            Console.WriteLine("Enter guessing interval start value:");
            var minNumberText = Console.ReadLine();
            isNumber = int.TryParse(minNumberText, out MIN_NUMBER);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
                  
        } while (!isNumber);

        do
        {
            Console.WriteLine("Enter guessing interval end value:");
            var maxNumberText = Console.ReadLine();
            isNumber = int.TryParse(maxNumberText, out MAX_NUMBER);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (MAX_NUMBER <= MIN_NUMBER)
            {
                Console.WriteLine("End value should be more than start value");
            }

        } while (!isNumber || MAX_NUMBER <= MIN_NUMBER);

        //подсчет количества попыток
        decimal middleIntervalValue = MAX_NUMBER;
        do
        {
            middleIntervalValue = (int)Math.Ceiling((middleIntervalValue - MIN_NUMBER + 1) / 2);
            MAX_ATTEMPT++;
        } while (middleIntervalValue > 1);
        //кто загадывает число - оператор или комп, если комп - то рандомизировать в выбранном диапазоне
        do
        {
            Console.WriteLine("Select who chooses magic number");
            Console.WriteLine("Enter '1' to select human opponent, enter '2' to select computer opponent");
            var opponentSelectedText = Console.ReadLine();
            isNumber = int.TryParse(opponentSelectedText, out opponentSelected);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (opponentSelected is not (1 or 2))
            {
                Console.WriteLine("PLease make valid selection: 1 or 2");
            }

        } while (!isNumber || opponentSelected is not (1 or 2));
        if (opponentSelected is 1)
        {
            do
            {
                Console.WriteLine($"User 1. Enter Magic number between [{MIN_NUMBER} and {MAX_NUMBER}]");

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
            userMagicNumber = Random.Shared.Next(MIN_NUMBER, MAX_NUMBER + 1); //рандомайзер
        }

        Console.Clear();
        var attempt = 0;
        int guess;
        var isWin = false;
        do
        {
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"Current guessing interval between [{MIN_NUMBER} and {MAX_NUMBER}]");   //выводить подсказку о диапазоне
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);

            if (guess > MAX_NUMBER)
            {
                Console.WriteLine("Entered value is bigger then end of interval"); //добавить проверку на то, что попытка в рамках диапазона
            }
            else if (guess < MIN_NUMBER)
            {
                Console.WriteLine("Entered value is smaller then start of interval");
            }
            else if (guess < userMagicNumber) 
            {
                Console.WriteLine("Our number is bigger");
                MIN_NUMBER = guess + 1;
                attempt++;
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                MAX_NUMBER = guess - 1;
                attempt++;
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
                attempt++;
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
