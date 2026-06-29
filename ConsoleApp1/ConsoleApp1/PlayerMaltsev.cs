using System;

namespace ConsoleApp1
{
    internal class PlayerMaltsev
    {
        // Просит игрока ввести число и возвращает его.
        // Возвращает -1, если ввод некорректный (не число).
        public int Guess()
        {
            var guessText = Console.ReadLine();
            if (int.TryParse(guessText, out int guess))
            {
                return guess;
            }
            return -1;
        }
    }
}