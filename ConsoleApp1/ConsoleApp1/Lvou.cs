class Kirilenko
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number");

        var MIN_NUMBER = 0;
        Random random = new Random();
        var userRange = 0;
        bool isNumber;
        Console.WriteLine("Write range for number from 0 to 2,147,483,647"); //Turns out you dont actualy need to check for int
        do
        {
            var range = Console.ReadLine();
            isNumber = int.TryParse(range, out userRange);

            if (!isNumber)
            {
                Console.WriteLine("It's not an int number");
            }
            else if (userRange < MIN_NUMBER)
            {
                Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");
            }
        } while (!isNumber
    || userRange < MIN_NUMBER);
        var user_type = 0;
        Console.WriteLine("Write 1 to play with computer, write 2 to play with another player");
        do
        {
            var input = Console.ReadLine();
            isNumber = int.TryParse(input, out user_type);

            if (!isNumber)
            {
                Console.WriteLine("It's not a number");
            }
            else if (user_type == 1 || user_type == 2)
            {
                Console.WriteLine("Thank you for your answer");
            }
            else if (user_type != 1 & user_type != 2)
            {
                Console.WriteLine($"Number is incorrect");
            }
        } while (!isNumber
    || user_type != 1
    & user_type != 2);
        int magicNumber = random.Next(0, userRange);
        if (user_type == 2)
        {
            Console.WriteLine($"Write number to guess from 0 to {userRange}"); //Turns out you dont actualy need to check for int
            do
            {
                var range = Console.ReadLine();
                isNumber = int.TryParse(range, out magicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (magicNumber < MIN_NUMBER)
                {
                    Console.WriteLine($"Too small number. Must be more or equal to {MIN_NUMBER}");
                }
                else if (magicNumber > userRange)
                {
                    Console.WriteLine($"Too big number. Must be less or equal to {userRange}");
                }
            } while (!isNumber
    || magicNumber < MIN_NUMBER
    || magicNumber > userRange);
        }
        Console.Clear();
        var attempts = 0;
        var NumberOfPower = 1;
        do
        {
            if (Math.Pow(2, NumberOfPower) <= userRange)
            {
                NumberOfPower++;
            }
            else if (Math.Pow(2, NumberOfPower) > userRange)
            {
                attempts = NumberOfPower;
            }
        } while (attempts == 0);
        var userLost = true;
        var userGuess = -1;
        if (Math.Pow(2, NumberOfPower) == userRange+1)
        {
            attempts++; 
        }
        var userMinimum = 0;
        var userMaximum = userRange;
        Console.WriteLine("The magic number is created. It is time, to guess!");
        do
        {
            Console.WriteLine($"Number is between {userMinimum} to {userMaximum}");
            Console.WriteLine($"Current attempts remain = {attempts}");
            var input = Console.ReadLine();
            isNumber = int.TryParse(input, out userGuess);
            if (!isNumber)
            {
                Console.WriteLine("It's not an int number. Please enter int number");
            }
            else if (userGuess > userMaximum)
            {
                Console.WriteLine($"Number out of your range. Try again");
            }
            else if (userGuess < userMinimum)
            {
                Console.WriteLine($"Number out of your range. Try again");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine($"Number too big. Try again");
                userMaximum = userGuess-1;
                attempts--;
            }
            else if (userGuess < magicNumber)
            {
                Console.WriteLine($"Number too Small. Try again");
                userMinimum = userGuess+1;
                attempts--;
            }
            else if (userGuess == magicNumber)
            {
                userLost = false;
            }
        } while (attempts > 0 && userLost);
        if (userLost == false)
        {
            Console.WriteLine($"That is correct! Winner winner chicken dinner! You had {attempts} attempts remaining when you guessed it!");
        }
        else if (attempts == 0)
        {
            Console.WriteLine($"You are out of attempts. You lost! The number was {magicNumber}");
        }
        //Console.WriteLine("Write 1 to play with computer, write 2 to play with another player");

        //do
        //{
        //    Console.WriteLine("User 1. Enter Magic number");

        //    var userMagicNumberText = Console.ReadLine();
        //    isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

        //    if (!isNumber)
        //    {
        //        Console.WriteLine("It's not a number");
        //    }
        //    else if (userMagicNumber > MAX_NUMBER)
        //    {
        //        Console.WriteLine($"Too big number. Must be less then {MAX_NUMBER}");
        //    }
        //    else if (userMagicNumber < MIN_NUMBER)
        //    {
        //        Console.WriteLine($"Too small number. Must be more then {MIN_NUMBER}");
        //    }
        //} while (!isNumber
        //    || userMagicNumber < MIN_NUMBER
        //    || userMagicNumber > MAX_NUMBER);

        //Console.Clear();
        //var attempt = 0;
        //int guess;
        //var isWin = false;
        //do
        //{
        //    attempt++;
        //    //Console.WriteLine("User 2. Enter your guess. Attemmpt [" + attempt + " / " + MAX_ATTEMPT + "]");
        //    Console.WriteLine($"User 2. Enter your guess. Attemmpt [{attempt} / {MAX_ATTEMPT}]");
        //    var guessText = Console.ReadLine();
        //    guess = int.Parse(guessText);

        //    if (guess < userMagicNumber)
        //    {
        //        Console.WriteLine("Our number is bigger");
        //    }
        //    else if (guess > userMagicNumber)
        //    {
        //        Console.WriteLine("Our number is less");
        //    }
        //    else if (guess == userMagicNumber)
        //    {
        //        isWin = true;
        //    }
        //} while (!isWin && attempt < MAX_ATTEMPT);


        //if (isWin)
        //{
        //    Console.WriteLine("Right! Your are Win");
        //}
        //else
        //{
        //    Console.WriteLine("Loooose");
        //}

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