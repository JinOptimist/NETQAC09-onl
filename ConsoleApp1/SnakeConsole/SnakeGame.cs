using System.Collections.Generic;
using System.Linq;

namespace SnakeConsole;

public class SnakeGame
{
    private int _width = 15;
    private int _height = 10;

    private Snake _snake = new Snake();
    private GameRenderer _renderer = new GameRenderer();

    private Random _random = new Random();
    private Point _food;
    private int _score = 0;

    public SnakeGame()
    {
        _food = CreateFood();
    }

    // Main game: draw, read input, check the next position, then move or grow
    public void Start()
    {
        while (true)
        {
            _renderer.Draw(_snake, _food, _score, _width, _height);

            // Reads one key per turn and converts it to a direction
            ReadDirectionFromKeyboard();

            // The next position is checked before changing the snake body
            Point nextHead = _snake.GetNextHead();

            if (IsWall(nextHead))
            {
                GameOver();
                break;
            }

            if (IsSnakeBody(nextHead))
            {
                GameOver();
                break;
            }

            if (IsFood(nextHead))
            {
                _snake.Grow();
                _score++;
                _food = CreateFood();
            }
            else
            {
                _snake.Move();
            }
        }

        Console.ReadKey();
    }

    private void ReadDirectionFromKeyboard()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

        switch (key.Key)
        {
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                _snake.ChangeDirection(Direction.Up);
                break;

            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                _snake.ChangeDirection(Direction.Down);
                break;

            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                _snake.ChangeDirection(Direction.Left);
                break;

            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                _snake.ChangeDirection(Direction.Right);
                break;
        }
    }

    // Walls
    private bool IsWall(Point point)
    {
        return point.X == 0
            || point.X == _width - 1
            || point.Y == 0
            || point.Y == _height - 1;
    }

    //The tail ignored cuz it moves away during a normal move
    private bool IsSnakeBody(Point point)
    {
        List<Point> bodyWithoutTail = new List<Point>();

        for (int i = 0; i < _snake.Body.Count - 1; i++)
        {
            bodyWithoutTail.Add(_snake.Body[i]);
        }

        return bodyWithoutTail.Any(bodyPoint => bodyPoint.X == point.X && bodyPoint.Y == point.Y);
    }

    private bool IsFood(Point point)
    {
        return point.X == _food.X && point.Y == _food.Y;
    }

    //Generating food until it appears outside the snake body
    private Point CreateFood()
    {
        Point food;

        do
        {
            food = new Point
            {
                X = _random.Next(1, _width - 1),
                Y = _random.Next(1, _height - 1)
            };
        }
        while (_snake.Body.Any(point => point.X == food.X && point.Y == food.Y));

        return food;
    }

    private void GameOver()
    {
        _renderer.Draw(_snake, _food, _score, _width, _height);
        Console.WriteLine("YOU DIED");
    }
}