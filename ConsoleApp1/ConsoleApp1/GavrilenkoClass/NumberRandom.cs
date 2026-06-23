//полуение заданного числа, если режим компа - генерируем число, если режим юзера - получаем число и возвращаем в gamegavr
namespace ConsoleApp1.GavrilenkoClass;

public class NumberRandom
{
    public int Generate(int min, int max, int mode)
    {
        if (mode == 2)//комп загадывает
            return new Random().Next(min, max + 1);

        Console.WriteLine("User: enter secret number");
        return new InputService().ReadGuess(min, max);
    }
}