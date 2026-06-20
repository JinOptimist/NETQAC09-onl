using System.ComponentModel.Design;

class Atyletskaya
{
    public void Do()
    {
        var MAX_ATTEMPT = 10;

        var userMagicNumber = 0;
        bool isMagicNumberCorrect;

        Console.WriteLine("The game Guess the number v1");


        //выбор мин числа

        var MIN_NUMBER = 1;
        bool isMinNumberCorrect;

        do
        {
            Console.WriteLine("Select min number");
            var userInputMinNumber = Console.ReadLine();
            isMinNumberCorrect = int.TryParse(userInputMinNumber, out MIN_NUMBER);

            if (!isMinNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
            }
            else if (MIN_NUMBER < 0)
            {
                Console.WriteLine("Min number must be 0 or greater");
            }

        }
        while (!isMinNumberCorrect || MIN_NUMBER < 0);


        //выбор макс числа

        var MAX_NUMBER = 100;
        bool isMaxNumberCorrect;

        do
        {
            Console.WriteLine("Select max number");
            var userInputMaxNumber = Console.ReadLine();
            isMaxNumberCorrect = int.TryParse(userInputMaxNumber, out MAX_NUMBER);

            if (!isMaxNumberCorrect)
            {
                Console.WriteLine("Please enter a number.");
            }
            else if (MIN_NUMBER >= MAX_NUMBER)
            {
                Console.WriteLine($"Max number must be greater than {MIN_NUMBER}");
            }
            else if (MIN_NUMBER == MAX_NUMBER - 1)
            {
                Console.WriteLine("There's no point to do so");

            }

        }
        while (!isMaxNumberCorrect || MIN_NUMBER >= MAX_NUMBER || MIN_NUMBER == MAX_NUMBER - 1);


        //выбор режима игры

        var selectedGameMode = 0;
        bool isGameModeCorrect;

     
        do
        {
            Console.WriteLine("Select game mode: Press 1 to play with a friend, Press 2 to enter training mode");
            var userInputGameMode = Console.ReadLine();
            isGameModeCorrect = int.TryParse(userInputGameMode, out selectedGameMode);

            if (!isGameModeCorrect)
            {
                Console.WriteLine("Please enter a number. Press 1 to play with a friend, Press 2 to enter training mode");
            }
            else if (selectedGameMode != 1 && selectedGameMode != 2)
            {
                Console.WriteLine("We have 2 modes only :( Press 1 to play with a friend, Press 2 to enter training mode");
            }
        }
        while (!isGameModeCorrect || (selectedGameMode != 1 && selectedGameMode != 2));

        if (selectedGameMode == 1)
        {
            do
            {
                Console.WriteLine("Enter Magic number");

                var userMagicNumberText = Console.ReadLine();
                isMagicNumberCorrect = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isMagicNumberCorrect)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > MAX_NUMBER)
                {
                    Console.WriteLine($"Can't be bigger than {MAX_NUMBER}");
                }
                else if (userMagicNumber < MIN_NUMBER)
                    {
                        Console.WriteLine($"Can't be less than {MIN_NUMBER}");
                    }
                } 
                while (!isMagicNumberCorrect
    || userMagicNumber < MIN_NUMBER
    || userMagicNumber > MAX_NUMBER);
            }

        
        else if (selectedGameMode == 2)
        {
            var random = new Random();
            userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER);
        }
        Console.Clear();


       

        //начало игры
        var attempt = 0;
        int guess;
        var isWin = false;

        do
        {
            Console.WriteLine($"Now guess the number. Attemmpt [{attempt} / {MAX_ATTEMPT}]");

            var guessText = Console.ReadLine();

            guess = int.Parse(guessText);

            if (guess > MAX_NUMBER || guess < MIN_NUMBER)
            {
                Console.WriteLine($"Reminder: you must guess number between {MIN_NUMBER} and {MAX_NUMBER}"); ;
            }
            else if (guess < userMagicNumber)
            {
                Console.WriteLine("Our number is bigger");
                attempt++;
            }
            else if (guess > userMagicNumber)
            {
                Console.WriteLine("Our number is less");
                attempt++;
            }

            else if (guess == userMagicNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < MAX_ATTEMPT);


        if (isWin)
        {
            Console.WriteLine("Right! You won");
        }
        else
        {
            Console.WriteLine($"Good luck next time. The number was {userMagicNumber}");
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