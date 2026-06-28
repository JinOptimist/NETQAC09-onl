using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class RandomLolita
    {
        private RandomLolita _random = new RandomLolita();

        // Метод принимает диапазон и возвращает одно случайное число
        public int GenerateNumber(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}
