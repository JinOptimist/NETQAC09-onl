public class HM_3
{ 
    public void Play()
    {
        Console.WriteLine("The game Guess the number");
        var rangeSelector = new RangeSelector();

        var rangeSettings = rangeSelector.SelectRange();
        Console.WriteLine($"You choose range {rangeSettings.minNumber} - {rangeSettings.maxNumber}. Maximum attempts: {rangeSettings.maxAttempt}");
        
        var gameModeSelector = new GameModeSelector();
        var gameModeChoice = gameModeSelector.SelectGameMode();
        
        Console.WriteLine($"You choose game mode {gameModeChoice}");
        
        var secretNumberCreator = new SecretNumberCreator();
        var secretNumber = secretNumberCreator.CreateSecretNumber(gameModeChoice, rangeSettings);
        
        var guessingProcessor = new GuessingProcessor();
        var isWin = guessingProcessor.StartGuessing(rangeSettings, secretNumber);
        
        var resultPrinter = new GameResultPrinter();
        resultPrinter.PrintResult(isWin);

    }

}