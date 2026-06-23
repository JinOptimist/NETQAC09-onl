//вычисление попыток Log2,игра, подсказки "больше-меньше",результаты игры
namespace ConsoleApp1.GavrilenkoClass;

public class GuessGame
{
    private readonly InputService _input = new();

    public void StartGame(int secret, int min, int max)
    {
        Console.Clear();

        int attempts = (int)Math.Ceiling(Math.Log2(max - min + 1));

        int currentMin = min;
        int currentMax = max;

        int attempt = 0;
        bool win = false;

        do
        {
            Console.WriteLine($"Range: {currentMin} - {currentMax}");
            Console.WriteLine($"Attempt [{attempt}/{attempts}]");

            int guess = _input.ReadGuess(currentMin, currentMax);

            if (guess < secret)
            {
                Console.WriteLine("More");
                currentMin = guess + 1;
            }
            else if (guess > secret)
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
            : $"Game over! You lost. The number was {secret}");
    }
}