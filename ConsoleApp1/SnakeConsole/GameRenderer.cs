using System.Linq;

namespace SnakeConsole;

public class GameRenderer
{
    // Draws the field row by row and column by column
    public void Draw(Snake snake, Point food, int score, int width, int height)
    {
        Console.Clear();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Drawing order matters: wall, head, body, food, empty space
                if (IsWall(x, y, width, height))
                {
                    Console.Write("#");
                }
                else if (snake.Body[0].X == x && snake.Body[0].Y == y)
                {
                    Console.Write("@");
                }
                else if (snake.Body.Any(point => point.X == x && point.Y == y))
                {
                    Console.Write("*");
                }
                else if (food.X == x && food.Y == y)
                {
                    Console.Write("F");
                }
                else
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine($"Score: {score}");
    }

   
    private bool IsWall(int x, int y, int width, int height)
    {
        return x == 0
            || y == 0
            || x == width - 1
            || y == height - 1;
    }
}