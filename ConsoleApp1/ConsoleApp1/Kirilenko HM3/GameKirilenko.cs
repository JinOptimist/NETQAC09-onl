class GameKirilenko
{
    public void MainKirilenkoProgram()
    {
        Console.WriteLine("The game Guess the number");
        var magicMaster = new Inputs().PlayerTypeInput();
        var maxRange = new Inputs().RangeInput();
        var magicNumber = new MagicNumber().MagicNumberGenerator(magicMaster, maxRange);
        var gameInput = new GameData(magicNumber, maxRange);
        var gameLoop = new GameCycle(gameInput);
        gameLoop.GameLoop();
    }
}