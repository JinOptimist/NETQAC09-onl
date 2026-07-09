// результат используется в SecretNumberCreator
public class GameModeSelector
{
    public int SelectGameMode()
    {
        var gameModeChoice = 0;
        bool isGameModeChoiceValid;

        do
        {
            Console.WriteLine("Choose the game mode. 1. You create the secret number; 2. Computer creates the secret number");
            var gameModeChoiceText = Console.ReadLine();
            isGameModeChoiceValid = int.TryParse(gameModeChoiceText, out gameModeChoice);

            if (!isGameModeChoiceValid)
            {
                Console.WriteLine("It's not a number");
            }
            else if (gameModeChoice < 1 || gameModeChoice > 2)
            {
                Console.WriteLine("Wrong choice. Must be 1 or 2");
            }
            else if (gameModeChoice == 1)
            {
                Console.WriteLine("You choose to create the secret number");
            }
            else if (gameModeChoice == 2)
            {
                Console.WriteLine("You choose to let computer create the secret number");
            }

        } while (!isGameModeChoiceValid || gameModeChoice < 1 || gameModeChoice > 2);

        return gameModeChoice;
    }
}