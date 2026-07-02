// класс выбора режима игры
namespace ConsoleApp1.GuessGameKrasnoperova;
class GameMode
    {
        public int SelectGameMode() 
        {
            int selectedGameMode;
            bool isGameModeCorrect;

            do
            {
                Console.WriteLine("Select game mode: Press 1 to play with a people, Press 2 to play with a bot");

                var inputGameMode = Console.ReadLine();
                isGameModeCorrect = int.TryParse(inputGameMode, out selectedGameMode);

                if (!isGameModeCorrect)
                {
                    Console.WriteLine("Please enter a number. Press 1 to play with a people, Press 2 to play with a bot");
                    continue;
                }
                else if (selectedGameMode != 1 && selectedGameMode != 2)
                {
                    Console.WriteLine("We have only 2 modes. Press 1 to play with a people, Press 2 to play with a bot");
                }
            }
            while (!isGameModeCorrect || (selectedGameMode != 1 && selectedGameMode != 2));

            return selectedGameMode;
        }

    }
