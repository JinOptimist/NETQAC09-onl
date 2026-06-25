using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Atyletskaya
{
    public class GameAttemptsCalculator
        //подсчет максимального количества попыток в выбранном диапазоне
    {
        public int CalculateMaxAttempts(int minNumber, int maxNumber)
        {
            var range = maxNumber - minNumber;
            var maxAttempts = (int)Math.Ceiling(Math.Log2(range));
            return maxAttempts;
        }
    }
}
