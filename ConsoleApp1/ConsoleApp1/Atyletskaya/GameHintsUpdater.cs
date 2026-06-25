using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApp1.Atyletskaya
{
    public class GameHintsUpdater
    {
        public int NewMin { get; set; }
        public int NewMax { get; set; }
        public void CheckNewGuess(int guess, int magicNumber)
        {
            if (guess < magicNumber && guess > NewMin)
            {
                NewMin = guess;
            }

            else if (guess > magicNumber && guess < NewMax)
            {
                NewMax = guess;
            }
        }
    }
}
