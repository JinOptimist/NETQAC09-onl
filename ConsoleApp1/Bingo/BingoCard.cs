using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    public class BingoCard
    {
        public int[,] Numbers { get; } = new int[5, 5];
        public bool[,] Marked { get; } = new bool[5, 5];
        private static readonly Random _rnd = new Random();

        public BingoCard()
        {
            Generate();
            Marked[2, 2] = true; // центр бесплатный
        }

        private void Generate()
        {
            // Колонки B(1-15) I(16-30) N(31-45) G(46-60) O(61-75)
            for (int col = 0; col < 5; col++)
            {
                var range = Enumerable.Range(col * 15 + 1, 15).OrderBy(_ => _rnd.Next()).Take(5).ToArray();
                for (int row = 0; row < 5; row++)
                    Numbers[row, col] = range[row];
            }
        }

        public void Mark(int number)
        {
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    if (Numbers[r, c] == number)
                        Marked[r, c] = true;
        }

        public bool CheckBingo()
        {
            // строки
            for (int r = 0; r < 5; r++)
            {
                var row = Enumerable.Range(0, 5).Select(c => Marked[r, c]);
                if (row.All(m => m)) return true;
            }
            // столбцы
            for (int c = 0; c < 5; c++)
            {
                var col = Enumerable.Range(0, 5).Select(r => Marked[r, c]);
                if (col.All(m => m)) return true;
            }
            // диагонали
            var diag1 = Enumerable.Range(0, 5).Select(i => Marked[i, i]);
            var diag2 = Enumerable.Range(0, 5).Select(i => Marked[i, 4 - i]);
            if (diag1.All(m => m) || diag2.All(m => m)) return true;

            return false;
        }

        public string Render()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(" B  I  N  G  O");
            sb.AppendLine(new string('-', 15));
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (r == 2 && c == 2)
                        sb.Append(" * ");
                    else if (Marked[r, c])
                        sb.Append($"[{Numbers[r, c],2}]");
                    else
                        sb.Append($"{Numbers[r, c],2} ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
