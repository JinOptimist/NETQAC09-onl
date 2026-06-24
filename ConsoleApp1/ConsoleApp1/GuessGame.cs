using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleApp1
{
    internal class GuessGame
    {
        public void Do()
        {
            Console.WriteLine("The game Guess the number");

            var MAX_ATTEMPT = 3;
            var MAX_NUMBER = 100;
            var MIN_NUMBER = 1;

            var userMagicNumber = 0;
            bool isNumber;

            Console.WriteLine("Кто загадывает число? 1 - Человек, 2 - Компьютер");
            var choice = Console.ReadLine();

            if (choice == "1")
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

            }
            else
            {

                var random = new Random();
                userMagicNumber = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
                Console.WriteLine("Компьютер загадал число! Нажмите Enter, чтобы начать.");
                Console.ReadLine();
            }

            Console.Clear();
            var attempt = 0;
            int guess;
            var isWin = false;

            // Создаем объект игрока из первого файла!
            var player = new Player();

            do
            {
                attempt++;
                Console.WriteLine($"User 2. Enter your guess. Attempt [{attempt} / {MAX_ATTEMPT}]");

                // Просим игрока сделать ход
                guess = player.Guess();

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
                Console.WriteLine("Right! You are Win");
            }
            else
            {
                Console.WriteLine("Loooose");
            }
        }
    }
}
