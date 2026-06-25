using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Atyletskaya
{
    public class GamePlayerMagicNumberSelector
    {
        public int SelectPlayerMagicNumber(int minNumber, int maxNumber)
        {
            var playersMagicNumber = 0;
            bool isMagicNumberCorrect;
            do
            {
                Console.WriteLine($"Select magic number between {minNumber} and {maxNumber}");
                var userInputMagicNumber = Console.ReadLine();
                isMagicNumberCorrect = int.TryParse(userInputMagicNumber, out playersMagicNumber);
                if (!isMagicNumberCorrect)
                {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }
                else if (playersMagicNumber < minNumber || playersMagicNumber > maxNumber)
                {
                    Console.WriteLine($"Please select a number between {minNumber} and {maxNumber}");
                }
            }
            while (!isMagicNumberCorrect || playersMagicNumber < minNumber || playersMagicNumber > maxNumber);
            return playersMagicNumber;
        }
    }
}
