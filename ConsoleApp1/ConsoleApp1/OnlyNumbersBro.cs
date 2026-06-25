namespace ConsoleApp1;
// класс для проверок чтоб нам фигню вместо числе не ввели
public class OnlyNumbersBro
{
    public int ReadNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out var result))
            {
                return result; 
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("HEY! ENTER A VALID NUMBER!!!");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}