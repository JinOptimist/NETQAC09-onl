namespace WheelOfFortune;
/// <summary>
/// Класс для анимации прокрута
/// </summary>
public class SpinAnimation
{
    public void PlayAnimation()
    {
        Console.ForegroundColor= ConsoleColor.Green;
        List<string> frames = new List<string> { "|", "/", "-", "\\" }; // кадры анимации
        Console.WriteLine("Крутим колесо: ");
        
        for (int i = 0; i < 20; i++)
        {
            Console.Write(frames[i % frames.Count]); // чтобы по достижении 4 кадра не переставали писать кадры анимации
            Thread.Sleep(100);
            Console.Write("\b"); // удаляет ранее записанный символ, чтоб выглядело как анимация
        }
        Console.ResetColor();
        Console.WriteLine("Стоп!");
        
    }
    public void CycleDivider()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("==================================");
        Console.WriteLine("Новый цикл кручения");
        Console.WriteLine("==================================");
        Console.ResetColor();
    }
}