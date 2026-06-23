using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class GameModeSelector
    {
        public int SelectGameMode()
        {
            var selectedGameMode = 0;
            bool isGameModeCorrect;

            do
            {
                Console.WriteLine("Select game mode: Press 1 to play with a friend, Press 2 to enter training mode");

                var userInputGameMode = Console.ReadLine();
                isGameModeCorrect = int.TryParse(userInputGameMode, out selectedGameMode);

                if (!isGameModeCorrect)
                {
                    Console.WriteLine("Please enter a number. Press 1 to play with a friend, Press 2 to enter training mode");
                    continue;
                }
                else if (selectedGameMode != 1 && selectedGameMode != 2)
                {
                    Console.WriteLine("We have 2 modes only :( Press 1 to play with a friend, Press 2 to enter training mode");
                }
            }
            while (!isGameModeCorrect || (selectedGameMode != 1 && selectedGameMode != 2));

            return selectedGameMode;
        }

    }
}
