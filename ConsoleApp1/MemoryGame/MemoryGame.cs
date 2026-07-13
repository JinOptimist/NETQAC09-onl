using System;
using System.Threading;

namespace MemoryGame;

public class MemoryGame
{
    private readonly MemoryBoard _board;
    private int _moveCount;

    public MemoryGame()
    {
        _board = new MemoryBoard();
        _moveCount = 0;
    }

    public void Start()
    {
        Console.WriteLine("=== Игра 'Найди пару' ===");
        Console.WriteLine("Найдите все пары одинаковых карточек!");
        Console.WriteLine();

        while (!_board.IsAllMatched())
        {
            _board.Display();
            Console.WriteLine();

            var (row1, col1) = GetCardInput("Введите координаты первой карточки (row col): ");
            if (!ValidateAndFlip(row1, col1))
                continue;

            _board.Display();
            Console.WriteLine();

            var (row2, col2) = GetCardInput("Введите координаты второй карточки (row col): ");
            if (!ValidateAndFlip(row2, col2, row1, col1))
            {
                _board.GetCard(row1, col1).State = CardState.Hidden;
                continue;
            }

            _board.Display();
            Console.WriteLine();

            _moveCount++;

            if (_board.CheckMatch(row1, col1, row2, col2))
            {
                Console.WriteLine("✓ Совпадение! Карточки остаются открытыми.");
            }
            else
            {
                Console.WriteLine("✗ Не совпало. Карточки закрываются...");
                Thread.Sleep(2000);
            }

            Console.WriteLine();
        }

        _board.Display();
        Console.WriteLine();
        Console.WriteLine($"🎉 Поздравляем! Вы нашли все пары за {_moveCount} ходов!");
    }

    private (int row, int col) GetCardInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (input?.Length == 2 &&
                int.TryParse(input[0], out int row) &&
                int.TryParse(input[1], out int col))
            {
                return (row, col);
            }

            Console.WriteLine("Неверный ввод. Введите два числа через пробел (например: 0 1)");
        }
    }

    private bool ValidateAndFlip(int row, int col, int? excludeRow = null, int? excludeCol = null)
    {
        if (!_board.IsValidPosition(row, col))
        {
            Console.WriteLine("Неверные координаты. Попробуйте снова.");
            return false;
        }

        if (excludeRow.HasValue && excludeCol.HasValue &&
            row == excludeRow.Value && col == excludeCol.Value)
        {
            Console.WriteLine("Вы уже выбрали эту карточку. Выберите другую.");
            return false;
        }

        var card = _board.GetCard(row, col);

        if (card.IsMatched)
        {
            Console.WriteLine("Эта карточка уже найдена. Выберите другую.");
            return false;
        }

        if (card.IsRevealed && !excludeRow.HasValue)
        {
            Console.WriteLine("Эта карточка уже открыта. Выберите другую.");
            return false;
        }

        _board.Flip(row, col);
        return true;
    }
}
