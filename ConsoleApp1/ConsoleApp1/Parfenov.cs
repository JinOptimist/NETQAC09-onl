using static System.Runtime.InteropServices.JavaScript.JSType;

class Parfenov
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");
        Console.WriteLine("set the range of natural numbers");

        var MAX_ATTEMPT = 10;
        var MIN_NUMBER = 0;
        bool isNumberMin;

        do
        {
            Console.WriteLine("Please enter the MIN_NUMBER");

            var MIN_NUMBER_Text = Console.ReadLine();
            isNumberMin = int.TryParse(MIN_NUMBER_Text, out MIN_NUMBER);

            if (!isNumberMin)
            {
                Console.WriteLine("It's not a number");
            }
            else if (MIN_NUMBER < 0)
            {
                Console.WriteLine("enter a number greater than 0");
            }

        } while (!isNumberMin
            || MIN_NUMBER < 0);


        var MAX_NUMBER = 0;
        bool isNumberMax;
        var minMaxNumber = MIN_NUMBER + 1;

        do
        {
            Console.WriteLine($"Please enter the MAX_NUMBER > {minMaxNumber}");

            var MAX_NUMBER_Text = Console.ReadLine();
            isNumberMax = int.TryParse(MAX_NUMBER_Text, out MAX_NUMBER);

            if (!isNumberMax)
            {
                Console.WriteLine("It's not a number");
            }
            else if (MAX_NUMBER <= minMaxNumber)
            {
                Console.WriteLine($"The number is too small. There must be more than {minMaxNumber}");
            }

        } while (!isNumberMax
            || MAX_NUMBER <= minMaxNumber);

        Console.WriteLine("will the user or randomizer guess the number?");
        bool isNumberFlow;
        var userMagicNumber = 0;
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
            else if (enterFlow == 2)
            {
                var random = new Random();
                userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER);
            }

        } while (enterFlow != 1 && enterFlow != 2);

        Console.Clear();
        var attempt = 0;
        int guess;
        var isWin = false;
        do
        {
            attempt++;
            //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
            Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);

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