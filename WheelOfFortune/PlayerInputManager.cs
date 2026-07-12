namespace WheelOfFortune;
/// <summary>
/// считываем с клавы решение игрока продолжать игру или нет
/// </summary>
public class PlayerInputManager
{
    public bool IWantToSpin()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Нажми [Enter] чтобы крутить или [Spacebar] чтобы выйти...");
        Console.ResetColor();
        while (true)
        {
            var key = Console.ReadKey(intercept: true).Key;
            if (key == ConsoleKey.Enter)
            {
                return true; // Продолжаем игру
            }
            else if (key == ConsoleKey.Spacebar)
            {
                return false; // Выходим
            }
            // Любые другие кнопки просто игнорируются
        }
    }
    
}