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
            Console.WriteLine($"Human creates the number. Please enter secret number from {rangeSettings.minNumber} to {rangeSettings.maxNumber}");
            var secretNumberText = Console.ReadLine();
            isSecretNumberValid = int.TryParse(secretNumberText, out secretNumber);
            if (!isSecretNumberValid)

            {
                Console.WriteLine("It's not a number");
            }
            else if (secretNumber < rangeSettings.minNumber || secretNumber > rangeSettings.maxNumber)

            {
                Console.WriteLine($"Wrong choice. Must be from {rangeSettings.minNumber} to {rangeSettings.maxNumber}");
            }



        } while (!isSecretNumberValid || secretNumber < rangeSettings.minNumber || secretNumber > rangeSettings.maxNumber);
        Console.Clear();
        return secretNumber;
    }
    private int CreateByComputer(GameRangeSettings rangeSettings)
    {
        var random = new Random();
        var secretNumber = random.Next(rangeSettings.minNumber, rangeSettings.maxNumber + 1);
        Console.WriteLine("Computer creates the number. Start guessing!");
        return secretNumber;

    }

}