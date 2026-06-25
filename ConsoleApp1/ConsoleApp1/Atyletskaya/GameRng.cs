using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Atyletskaya
{
    internal class GameRng

    {
        public int GenerateRandomNumber(int minNumber, int maxNumber)
        {
            Random randomMagicNumber = new Random();
            return randomMagicNumber.Next(minNumber, maxNumber + 1);
        }
    }
}
