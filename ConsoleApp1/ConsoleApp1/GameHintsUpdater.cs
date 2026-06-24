using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp1
{
    public class GameHintsUpdater
    {
        public int newMin;
        public int newMax;
        public void CheckNewGuess(int guess, int magicNumber)
        {
            if (guess < magicNumber && guess > newMin)
            {
                newMin = guess;
            }

            else if (guess > magicNumber && guess < newMax)
            {
                newMax = guess;
            }
        }
    }
}
