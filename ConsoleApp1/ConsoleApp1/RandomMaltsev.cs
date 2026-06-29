using System;

namespace ConsoleApp1
{
    internal class RandomMaltsev
    {
        private Random _random = new Random();

        // Принимает диапазон и возвращает одно случайное число
        public int GenerateNumber(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}