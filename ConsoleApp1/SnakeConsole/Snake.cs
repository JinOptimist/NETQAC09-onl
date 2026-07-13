using System.Collections.Generic;

namespace SnakeConsole;

public class Snake
{
    // The first item in Body is always the snake head
    public List<Point> Body { get; set; } = new List<Point>();

    public Direction Direction { get; set; } = Direction.Right;

    // The snake starts with three segments and moves to the right
    public Snake()
    {
        Body.Add(new Point { X = 7, Y = 5 });
        Body.Add(new Point { X = 6, Y = 5 });
        Body.Add(new Point { X = 5, Y = 5 });
    }

    // Prevents the snake from turning directly into itself
    public void ChangeDirection(Direction newDirection)
    {
        if (IsOppositeDirection(newDirection))
        {
            return;
        }

        Direction = newDirection;
    }

    // Checks if the new direction is opposite to the current direction
    private bool IsOppositeDirection(Direction newDirection)
    {
        switch (Direction)
        {
            case Direction.Up:
                return newDirection == Direction.Down;
            case Direction.Down:
                return newDirection == Direction.Up;
            case Direction.Left:
                return newDirection == Direction.Right;
            case Direction.Right:
                return newDirection == Direction.Left;
        }

        return false;
    }

    // Calculates the next head position before the snake moves
    public Point GetNextHead()
    {
        Point head = Body[0];

        Point nextHead = new Point
        {
            X = head.X,
            Y = head.Y
        };

        switch (Direction)
        {
            case Direction.Up:
                nextHead.Y--;
                break;
            case Direction.Down:
                nextHead.Y++;
                break;
            case Direction.Left:
                nextHead.X--;
                break;
            case Direction.Right:
                nextHead.X++;
                break;
        }

        return nextHead;
    }

    // Normal move, add a new head and remove the tail, the length stays the same
    public void Move()
    {
        Point nextHead = GetNextHead();

        Body.Insert(0, nextHead);
        Body.RemoveAt(Body.Count - 1);
    }

    // Grow move,add a new head and keep the tail, the snake becomes longer
    public void Grow()
    {
        Point nextHead = GetNextHead();

        Body.Insert(0, nextHead);
    }
}