// Создаёт secretNumber по выбранному деапозону результат используется в GuessingProcessor.
public class SecretNumberCreator
{ 
    public int CreateSecretNumber(int gameModeChoice, GameRangeSettings rangeSettings)
    {
        if (gameModeChoice == 1)
        {
            return CreateByHuman(rangeSettings);
        }
        return CreateByComputer(rangeSettings);
    }
    private int CreateByHuman(GameRangeSettings rangeSettings)
    {
        var secretNumber = 0;
        bool isSecretNumberValid;
        do
        {
            Console.WriteLine($"Human creates the number. Please enter secret number from {rangeSettings.MinNumber} to {rangeSettings.MaxNumber}");
            var secretNumberText = Console.ReadLine();
            isSecretNumberValid = int.TryParse(secretNumberText, out secretNumber);
            if (!isSecretNumberValid)

            {
                Console.WriteLine("It's not a number");
            }
            else if (secretNumber < rangeSettings.MinNumber || secretNumber > rangeSettings.MaxNumber)

            {
                Console.WriteLine($"Wrong choice. Must be from {rangeSettings.MinNumber} to {rangeSettings.MaxNumber}");
            }



        } while (!isSecretNumberValid || secretNumber < rangeSettings.MinNumber || secretNumber > rangeSettings.MaxNumber);
        Console.Clear();
        return secretNumber;
    }
    private int CreateByComputer(GameRangeSettings rangeSettings)
    {
        var random = new Random();
        var secretNumber = random.Next(rangeSettings.MinNumber, rangeSettings.MaxNumber + 1);
        Console.WriteLine("Computer creates the number. Start guessing!");
        return secretNumber;

    }

}