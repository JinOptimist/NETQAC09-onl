namespace oop_core;

public class Boss : GameCharacter
{
    public int Armor;
    
    public override void GameCharacterPrintInfo()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        base.GameCharacterPrintInfo();
        Console.WriteLine($"Boss Armor: {Armor}");
        Console.ResetColor();
    }
}