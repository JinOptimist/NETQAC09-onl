using System;
using System.Collections.Generic;

namespace MemoryGame;

public class MemoryBoard
{
    private readonly MemoryCard[,] _cards;
    private readonly int _rows = 4;
    private readonly int _cols = 4;

    public MemoryBoard()
    {
        _cards = new MemoryCard[_rows, _cols];
        InitializeCards();
        Shuffle();
    }

    private void InitializeCards()
    {
        char[] symbols = { 'A', 'A', 'B', 'B', 'C', 'C', 'D', 'D', 'E', 'E', 'F', 'F', 'G', 'G', 'H', 'H' };
        int index = 0;

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                _cards[row, col] = new MemoryCard(symbols[index++]);
            }
        }
    }

    private void Shuffle()
    {
        var random = new Random();
        var flatList = new List<MemoryCard>();

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                flatList.Add(_cards[row, col]);
            }
        }

        for (int i = flatList.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (flatList[i], flatList[j]) = (flatList[j], flatList[i]);
        }

        int index = 0;
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                _cards[row, col] = flatList[index++];
            }
        }
    }

    public bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < _rows && col >= 0 && col < _cols;
    }

    public MemoryCard GetCard(int row, int col)
    {
        return _cards[row, col];
    }

    public void Flip(int row, int col)
    {
        var card = _cards[row, col];
        if (card.State == CardState.Hidden)
        {
            card.State = CardState.Revealed;
        }
    }

    public bool CheckMatch(int row1, int col1, int row2, int col2)
    {
        var card1 = _cards[row1, col1];
        var card2 = _cards[row2, col2];

        if (card1.Symbol == card2.Symbol)
        {
            card1.State = CardState.Matched;
            card2.State = CardState.Matched;
            return true;
        }

        card1.State = CardState.Hidden;
        card2.State = CardState.Hidden;
        return false;
    }

    public bool IsAllMatched()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                if (!_cards[row, col].IsMatched)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Display()
    {
        Console.Write("  ");
        for (int col = 0; col < _cols; col++)
        {
            Console.Write($"{col} ");
        }
        Console.WriteLine();

        for (int row = 0; row < _rows; row++)
        {
            Console.Write($"{row} ");
            for (int col = 0; col < _cols; col++)
            {
                var card = _cards[row, col];
                if (card.IsMatched)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"{card.Symbol} ");
                    Console.ResetColor();
                }
                else if (card.IsRevealed)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"{card.Symbol} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("# ");
                }
            }
            Console.WriteLine();
        }
    }
}
