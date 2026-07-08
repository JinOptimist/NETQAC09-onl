using System;

namespace ConsoleApp1
{
    internal class GameMaltsev
    {
        public void Do()
        {
            Console.WriteLine("Welcome to the toxic game \"Guess the number\"");

            var MAX_ATTEMPT = 7;
            var MAX_NUMBER = 99;
            var MIN_NUMBER = 1;

            var magicNumber = 0;
            bool isNumber;

            Console.WriteLine($"Hello! Do you want to play by yourself or let the computer generate the number? Enter 1 for self and 2 for computer. The number must be between {MIN_NUMBER} and {MAX_NUMBER}");
            var userChoice = Console.ReadLine();

            if (userChoice == "2")
            {
                // Создаём объект класса-генератора
                var random = new RandomMaltsev();
                //  Просим его сгенерировать число
                magicNumber = random.GenerateNumber(MIN_NUMBER, MAX_NUMBER);
                Console.WriteLine("The computer picked a number! Press Enter to start.");
                Console.ReadLine();
            }
            else
            {
                // пользователь вводит число сам с проверкой)
                do
                {
                    Console.WriteLine("User 1. Enter your magic number:");
                    var text = Console.ReadLine();
                    isNumber = int.TryParse(text, out magicNumber);

                    if (!isNumber)
                    {
                        Console.WriteLine("It's not a number dude");
                    }
                    else if (magicNumber > MAX_NUMBER)
                    {
                        Console.WriteLine($"Too big number. Must be less than {MAX_NUMBER}");
                    }
                    else if (magicNumber < MIN_NUMBER)
                    {
                        Console.WriteLine($"Too small number. Must be more than {MIN_NUMBER}");
                    }
                } while (!isNumber
                    || magicNumber < MIN_NUMBER
                    || magicNumber > MAX_NUMBER);
            }

            Console.Clear();

            var attempt = 0;
            var isWin = false;

            // Создаём объект игрока
            var player = new PlayerMaltsev();

            do
            {
                attempt++;
                Console.WriteLine($"User 2. Enter your guess. Attempt [{attempt} / {MAX_ATTEMPT}]");

                // Просим игрока сделать ход
                int guess = player.Guess();

                if (guess == -1)
                {
                    Console.WriteLine("It's not a number dude");
                    attempt--; // не считаем некорректный ввод за попытку
                    continue;
                }

                if (guess < magicNumber)
                {
                    Console.WriteLine("Our number is bigger");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Our number is less");
                }
                else
                {
                    isWin = true;
                }
            } while (!isWin && attempt < MAX_ATTEMPT);

            if (isWin)
            {
                Console.WriteLine("Right! You have won!");
            }
            else
            {
                Console.WriteLine("Loooose");
            }
        }
    }
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


    
