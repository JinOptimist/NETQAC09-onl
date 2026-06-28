using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class PlayerLolita
    {
        // Метод просит пользователя ввести догадку и возвращает её в виде числа
        public int Guess()
        {
            var guessText = Console.ReadLine();
            return int.Parse(guessText);
        }

    }
}
