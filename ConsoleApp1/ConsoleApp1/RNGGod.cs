using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class RNGGod

    {
        public int GenerateRandomNumber(int minNumber, int maxNumber)
        {
            Random randomMagicNumber = new Random();
            return randomMagicNumber.Next(minNumber, maxNumber + 1);
        }
    }
}
