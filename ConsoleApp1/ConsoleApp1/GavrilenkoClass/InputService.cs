//все что касается вывода и ввода данных
namespace ConsoleApp1.GavrilenkoClass;

public class InputService
{
    public Range ReadRange()//получение диаппазона
    {
        Console.Write("Enter MIN number: ");
        int min = int.Parse(Console.ReadLine()!);

        Console.Write("Enter MAX number: ");
        int max = int.Parse(Console.ReadLine()!);

        return new Range(min, max); //возврат с введенным знаением
    }

    public int ReadMode() //ктозагадывает число
    {
        Console.WriteLine("Who will guess the number?");
        Console.WriteLine("1 - User");
        Console.WriteLine("2 - Computer");

        return int.Parse(Console.ReadLine()!);
    }

    public int ReadGuess(int min, int max)//получаем число от пользователя
    {
        int number;

        while (true)
        {
            Console.WriteLine($"Enter number from {min} to {max}"); //подсказки

            if (int.TryParse(Console.ReadLine(), out number))
                return number;

            Console.WriteLine("It's not a number");
        }
    }
}