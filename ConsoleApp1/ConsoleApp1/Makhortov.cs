class makhortov
{
    public void Do()
    {
        Console.WriteLine("The game Guess the number\n");
        
        //0) Вначале давать выбор в каком диапазоне будет число
        Console.WriteLine("Enter the range which will be used for setting the Magic number up and provided to the Guesser:");
        Console.WriteLine("Enter the minimal border of a range");
        var minBorderString = Console.ReadLine();
        var minBorderValue = int.Parse(minBorderString);
        
        Console.WriteLine("\nEnter the maximal border of a range");
        var maxBorderString = Console.ReadLine();
        var maxBorderValue = int.Parse(maxBorderString);

        //1) Вычислять максимальное количество попыток (с учётом того что игрок использует метод деления отрезка пополам)
        var attempts = (int)Math.Ceiling(Math.Log2(maxBorderValue - minBorderValue)); //взял строку из инета
        Console.WriteLine("\nPossible attempts count for guesser: {0}", attempts);
        
        //2) Сделать вначале выбор, число загадывает человек или компьютер
        Console.WriteLine("\nWho will set the Magic number up? Enter 'c' for computer or 'h' for human");
        var isValidChoose = false;
        var riddlerChar = '0'; 
        do
        {
            riddlerChar = char.Parse(Console.ReadLine());
            switch (riddlerChar)
            {
                case 'c': 
                    Console.WriteLine("You have chosen computer as a riddler");
                    isValidChoose = true;
                    break;
                case 'h': 
                    Console.WriteLine("You have chosen human as a riddler");
                    isValidChoose = true;
                    break;
                default: 
                    Console.WriteLine("You have entered unsupported letter. Try one more time. Enter 'c' for computer or 'h' for human");
                    isValidChoose = false;
                    break;
            }
        } while (!isValidChoose);

        var userMagicNumber = 0;
        if (riddlerChar == 'c')
        {
            userMagicNumber = new Random().Next(minBorderValue, maxBorderValue);
        }
        else
        {
            bool isNumber;
            do
            {
                Console.WriteLine("\nEnter Magic number");
                var userMagicNumberText = Console.ReadLine();
                isNumber = int.TryParse(userMagicNumberText, out userMagicNumber);

                if (!isNumber)
                {
                    Console.WriteLine("It's not a number");
                }
                else if (userMagicNumber > maxBorderValue)
                {
                    Console.WriteLine($"Too big number. Must be less then {maxBorderValue}");
                }
                else if (userMagicNumber < minBorderValue)
                {
                    Console.WriteLine($"Too small number. Must be more then {minBorderValue}");
                }
            } while (!isNumber || userMagicNumber < minBorderValue || userMagicNumber > maxBorderValue);
        }
        Console.Clear();
        
        
        var attempt = 0;
        int guess;
        var isWin = false;
        do
        {
            attempt++;
            Console.WriteLine($"Enter your guess. Attempt [{attempt} / {attempts}]");
            var guessText = Console.ReadLine();
            guess = int.Parse(guessText);
            
            //3) Выводить подсказки о диапазоне для игрока, после каждого хода
            if (guess < userMagicNumber && guess > minBorderValue)
            {
                Console.WriteLine($"Our number is bigger. Try something from range: [{++guess};{maxBorderValue}]\n");
            }
            else if (guess > userMagicNumber && guess < maxBorderValue)
            {
                Console.WriteLine($"Our number is less. Try something from range: [{minBorderValue};{--guess}]\n");
            }
            //4) Не считать за попытку, если число вне разрешённого диапазона
            else if (guess > maxBorderValue || guess < minBorderValue)
            {
                Console.WriteLine($"Your number is out of range. Try something from range: [{minBorderValue};{maxBorderValue}]\n");
                attempt--;
            }
            else if (guess == userMagicNumber)
            {
                isWin = true;
            }
        } while (!isWin && attempt < attempts);
        
        if (isWin)
        {
            Console.WriteLine("Right! Your are winner");
        }
        else
        {
            Console.WriteLine("You have lost. The Magic number is {0}", userMagicNumber);
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