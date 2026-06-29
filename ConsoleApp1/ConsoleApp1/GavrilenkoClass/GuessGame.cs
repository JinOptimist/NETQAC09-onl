//вычисление попыток Log2,игра, подсказки "больше-меньше",результаты игры
namespace ConsoleApp1.GavrilenkoClass;

public class GuessGame
{
    private InputService _input = new();

    public void StartGame(int secretNumber, int min, int max)
    {
        Console.Clear();

        var attempts = (int)Math.Ceiling(Math.Log2(max - min + 1));

        var currentMin = min;
        var currentMax = max;

        var attempt = 0;
        var win = false;

        do
        {
            Console.WriteLine($"Range: {currentMin} - {currentMax}");
            Console.WriteLine($"Attempt [{attempt}/{attempts}]");

            var guess = _input.ReadGuess(currentMin, currentMax);

            if (guess < secretNumber)
            {
                Console.WriteLine("More");
                currentMin = guess + 1;
            }
            else if (guess > secretNumber)
            {
                Console.WriteLine("Less");
                currentMax = guess - 1;
            }
            else
            {
                win = true;
            }

            attempt++;

        } while (!win && attempt < attempts);

        Console.WriteLine(win
            ? "Game over!You win!"
            : $"Game over! You lost. The number was {secretNumber}");
    }
}